namespace Alpha.Services.API.Infrastructure.Exceptions
{
    public class ErrorResponse
    {
        public string[] Messages { get; set; }

        public object DeveloperMessage { get; set; }
    }
}