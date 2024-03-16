using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Common.Models
{
    public enum CommonErrorEnum
    {
        #region BadRequest
        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "An item is null")]
        ItemNull,
        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "The sequence contains no items")]
        NoItems,
        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "The rates to calculate")]
        NoRates
        #endregion
    }
}
