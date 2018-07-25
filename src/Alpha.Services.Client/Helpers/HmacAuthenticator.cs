namespace Alpha.Services.Client.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using Alpha.Services.Client.Configurations;
    using RestSharp;
    using RestSharp.Authenticators;

    public class HmacAuthenticator : IAuthenticator
    {
        private const string AppId = "1e8cf0fb1bb94c9a8180cdf91d8d39be";

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var requestUri = HttpUtility.UrlEncode(client.BuildUri(request).OriginalString.ToLower());
            string requestHttpMethod = request.Method.ToString();

            //Calculate UNIX time
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

            //create random nonce for each request
            string nonce = Guid.NewGuid().ToString("N");

            //Creating the raw signature string
            string signatureRawData = $"{AppId}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";

            var secretKeyByteArray = Convert.FromBase64String(AppSettings.ApiKey);

            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

                // Setting the values in the Authorization header using custom scheme (amx)
                var authHeader = $"amx {AppId}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}";
                request.AddParameter("Authorization", authHeader, ParameterType.HttpHeader);
            }
        }
    }
}