using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Common.Models
{
    public class InternalErorr
    {
        public InternalErorr(string errorCodeAsString, string? addtionalMessage, string? diagnosticMessage)
        {
            Code = errorCodeAsString;
            Message = addtionalMessage;
            DiagnosticMessage = diagnosticMessage;
        }

        public string Code { get; set; }
        public string? Message { get; set; }
        public string? DiagnosticMessage { get; set; }

        public string ToStringForLogging() => $"Code {Code} \r\nMessage {Message} \r\nDiagnostic Message {DiagnosticMessage}";
    }
}
