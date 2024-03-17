using TaxCalculator.Common.Attributes;

namespace TaxCalculator.Common.Models
{
    public enum CommonErrorEnum
    {
        #region BadRequest

        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "An item is null.")]
        ItemNull,

        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "The rates to calculate.")]
        NoRates,

        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "The ID passed is incorrect.")]
        EmptyResourceId,

        #endregion

        #region NotFound

        [ErrorMappings(System.Net.HttpStatusCode.NotFound, "The sequence contains no items.")]
        NoItems,

        [ErrorMappings(System.Net.HttpStatusCode.NotFound, "An item has not beed found.")]
        ItemNotFound,
        
        [ErrorMappings(System.Net.HttpStatusCode.NotFound, "An item has not beed found to revome.")]
        RemoveUnsuccessful,

        #endregion
    }
}
