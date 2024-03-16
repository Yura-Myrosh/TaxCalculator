using System.Net;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Common.Implementation
{
    public class ErrorManager<T> : IErrorManager<T> where T : Enum
    {
        private readonly Dictionary<T, ErrorMappingsAttribute> _errorMappingByEnum = new Dictionary<T, ErrorMappingsAttribute>();

        public ErrorManager()
        {
            var errorMapping = Enum.GetValues(typeof(T));

            foreach (T error in errorMapping)
            {
                var attributes = (ErrorMappingsAttribute[])error.GetType().GetField(error.ToString())
                    .GetCustomAttributes(typeof(ErrorMappingsAttribute), false);

                if (attributes.Length > 0)
                {
                    _errorMappingByEnum[error] = attributes[0];
                }
            }
        }

        public ApiException BuildException(T errorEnumValue, string? additionalMessage = null)
        {
            return BuildFormattedException(errorEnumValue, additionalMessage);
        }

        public ApiException BuildFormattedException(T errorEnum, string? addtionalMessage = null, params object?[] args)
        {
            GetErrorMapppings(errorEnum, out HttpStatusCode httpStatusCode, out string errorMessage);

            return new ApiException(httpStatusCode, errorEnum.ToString(), args == null ? errorMessage : string.Format(errorMessage, args), addtionalMessage);
        }

        private void GetErrorMapppings(T errorEnumValue, out HttpStatusCode httpStatusCode, out string errorMessage)
        {
            if (_errorMappingByEnum.TryGetValue(errorEnumValue, out var mapping) && mapping != null)
            {
                httpStatusCode = mapping.HttpStatusCode;
                errorMessage = mapping.ErrorMessage;
            }
            else
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
                errorMessage = string.Empty;
            }
        }
    }
}
