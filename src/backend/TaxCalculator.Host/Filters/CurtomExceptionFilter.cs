using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using TaxCalculator.Common.Models;
using TaxCalculator.Host.Models;

namespace TaxCalculator.Host.Filters
{
    public class CurtomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ServiceErrorResponce();

            if (context.Exception is ApiException apiException)
            {
                response.Error = new ServiceError
                {
                    Id = string.Empty,
                    Status = apiException.HttpStatusCode.ToString(),
                    Message = apiException.Message,
                    Code = apiException.Error.Code
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)apiException.HttpStatusCode
                };
            }
            else
            {
                var exception = context.Exception;
                response.Error = new ServiceError
                {
                    Id = string.Empty,
                    Message = exception.Message,
                    Code = exception.TargetSite?.ToString() ?? string.Empty
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
