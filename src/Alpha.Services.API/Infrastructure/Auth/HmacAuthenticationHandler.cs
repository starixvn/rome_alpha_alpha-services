namespace Alpha.Services.API.Infrastructure.Auth
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using System.Web;
    using Alpha.Services.Configurations;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal class HmacAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const long RequestMaxAgeInSeconds = 300;
        private const string AuthenticationScheme = "amx";

        private readonly IMemoryCache memoryCache;
        private readonly Fanex.Logging.ILogger logger;
        private readonly IAppSettings appSettings;

        public HmacAuthenticationHandler(
            IMemoryCache memoryCache,
            Fanex.Logging.ILogger logger,
            IAppSettings appSettings,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, loggerFactory, encoder, clock)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.appSettings = appSettings;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = (HttpRequestHeaders)Context.Request.Headers;
            var authorizations = headers.HeaderAuthorization;

            if (authorizations.Any())
            {
                var authorization = authorizations.First();

                if (authorization.StartsWith(AuthenticationScheme))
                {
                    var autherizationHeaderArray = GetAutherizationHeaderValues(authorization);

                    if (autherizationHeaderArray != null)
                    {
                        var appId = autherizationHeaderArray[0];
                        var incomingBase64Signature = autherizationHeaderArray[1];
                        var nonce = autherizationHeaderArray[2];
                        var requestTimeStamp = autherizationHeaderArray[3];

                        var isValid = IsValidRequest(Context.Request, appId, incomingBase64Signature, nonce, requestTimeStamp);

                        if (isValid)
                        {
                            var identity = new ClaimsIdentity("HMAC");
                            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), "HMAC");

                            return Task.FromResult(AuthenticateResult.Success(ticket));
                        }

                        return Task.FromResult(AuthenticateResult.Fail("Incorrect 'Authorization' header."));
                    }
                }
            }

            if (appSettings.DisableAuthentication)
            {
                logger.Info("Missing or malformed 'Authorization' header.");

                var identity = new ClaimsIdentity("HMAC");
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), "HMAC");

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Missing or malformed 'Authorization' header."));
        }

        private static string[] GetAutherizationHeaderValues(string rawAuthzHeader)
        {
            var credArray = rawAuthzHeader.Split(' ')[1].Split(':');
            return credArray.Length == 4 ? credArray : null;
        }

        private bool IsValidRequest(HttpRequest request, string appId, string incomingBase64Signature, string nonce,
            string requestTimeStamp)
        {
            string requestUri = HttpUtility.UrlEncode(request.Scheme + "://" + request.Host + request.PathBase + request.Path).ToLowerInvariant();
            string requestHttpMethod = request.Method;

            if (IsReplayRequest(nonce, requestTimeStamp))
            {
                return false;
            }

            var signatureRawData = $"{appId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";

            var sharedKey = "vvVAsk+7swmv0F1Zstz73B54B6Qeroksvt2l2TIqy98=";
            var secretKeyByteArray = Convert.FromBase64String(sharedKey);

            var isValid = false;
            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                isValid = incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal);
            }

            if (!isValid && appSettings.DisableAuthentication)
            {
                logger.Info($"SignatureRawData: {signatureRawData}");
                isValid = true;
            }

            return isValid;
        }

        private bool IsReplayRequest(string nonce, string requestTimeStamp)
        {
            if (memoryCache.TryGetValue(nonce, out object _))
            {
                logger.Info("Request is a replay attack");

                return true;
            }

            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var currentTimeSpan = DateTime.UtcNow - epochStart;

            var serverTotalSeconds = Convert.ToUInt64(currentTimeSpan.TotalSeconds);
            var requestTotalSeconds = Convert.ToUInt64(requestTimeStamp);

            if (((long)serverTotalSeconds - (long)requestTotalSeconds) > RequestMaxAgeInSeconds)
            {
                logger.Info($"Server Total Seconds: {serverTotalSeconds}"
                        + Environment.NewLine
                        + $"Request Total Seconds: {requestTotalSeconds}");

                return true;
            }

            memoryCache.Set(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(RequestMaxAgeInSeconds));

            return false;
        }
    }
}