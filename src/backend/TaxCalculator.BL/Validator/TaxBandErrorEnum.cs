using TaxCalculator.Common.Attributes;

namespace TaxCalculator.BL.Validator
{
    public enum TaxBandErrorEnum
    {
        #region BadRequest

        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "Salary couldn't be less than zero")]
        InvalidSalaryValue,

        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "Upper bound couldn't be less than lower.")]
        InvalidBounds

        #endregion
    }
}