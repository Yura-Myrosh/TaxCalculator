using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Common.Attributes;

namespace TaxCalculator.Common.Models
{
    public class ApiException : Exception
    {
        private readonly int _httpStatusCode;

        public ApiException(HttpStatusCode httpStatusCode, string errorCodeAsString, string errorMessage, string? addtionalMessage = null, string? diagnisticalMessage = null)
            : base(errorMessage + (!string.IsNullOrEmpty(addtionalMessage) ? " " + addtionalMessage : string.Empty))
        {
            Error = new InternalErorr(errorCodeAsString, errorMessage + (!string.IsNullOrEmpty(addtionalMessage) ? " " + addtionalMessage : string.Empty), diagnisticalMessage);
            _httpStatusCode = (int)httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get =>  (HttpStatusCode)_httpStatusCode; }

        public InternalErorr Error { get; set; }
    }
}
