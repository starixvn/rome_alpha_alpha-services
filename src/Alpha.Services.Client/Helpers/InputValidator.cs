namespace Alpha.Services.Client.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class InputValidator
    {
        public static void ValidateInputParam(string paramName, int paramValue)
        {
            if (paramValue <= 0)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ValidateInputParam(string paramName, string paramValue)
        {
            if (string.IsNullOrWhiteSpace(paramValue))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ValidateInputParam<T>(string paramName, IEnumerable<T> paramValue)
        {
            if (paramValue == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (!paramValue.Any())
            {
                throw new ArgumentOutOfRangeException($"{paramName} does not contain any item");
            }
        }
    }
}