using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Common.Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ErrorMappingsAttribute : Attribute
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ErrorMessage { get; }

        public ErrorMappingsAttribute(HttpStatusCode httpStatusCode, string errorMessage)
        {
            HttpStatusCode = httpStatusCode;
            ErrorMessage = errorMessage;
        }
    }
}
