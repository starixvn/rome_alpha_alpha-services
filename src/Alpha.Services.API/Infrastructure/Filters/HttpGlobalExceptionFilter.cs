namespace Alpha.Services.API.Infrastructure.Filters
{
    using System.Linq;
    using System.Net;
    using Alpha.Services.API.Extensions;
    using Alpha.Services.API.Infrastructure.ActionResults;
    using Alpha.Services.API.Infrastructure.Exceptions;
    using Fanex.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.Error(BuildErrorMessage(context));

            if (context.Exception.GetType() == typeof(DomainException))
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                problemDetails.Errors.Add("DomainValidations", new[] { context.Exception.Message });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new ErrorResponse
                {
                    Messages = new[] { "An error ocurred." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private static string BuildErrorMessage(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            var bodyData = request.GetRawBodyStringAsync().Result;
            var path = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            var headers = string.Join(
                "\r\n",
                request.Headers.Select(header => $"{header.Key}:{header.Value}"));
            var cookies = string.Join(
                "\r\n",
                request.Cookies.Select(cookie => $"{cookie.Key}:{cookie.Value}"));

            return string.Join(
                "\r\n\r\n",
                context.Exception.Message,
                $"PATH: {path}",
                $"BODY: {bodyData}",
                $"HEADERS: \r\n {headers}",
                $"COOKIES: \r\n {cookies}",
                $"EXCEPTION: {context.Exception}");
        }
    }
}