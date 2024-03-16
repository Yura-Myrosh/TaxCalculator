using TaxCalculator.Common.Models;

namespace TaxCalculator.BL.Validator
{
    public enum TaxBandErrorEnum
    {
        #region BadRequest
        [ErrorMappings(System.Net.HttpStatusCode.BadRequest, "Salary couldn't be less than zero")]
        InvalidSalaryValue
        #endregion
    }
}
