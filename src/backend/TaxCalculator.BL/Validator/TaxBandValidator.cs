using TaxCalculator.Common.Interfaces;

namespace TaxCalculator.BL.Validator
{
    public class TaxBandValidator : ITaxBandValidator
    {
        private readonly IErrorManager<TaxBandErrorEnum> _errorManager;

        public TaxBandValidator(IErrorManager<TaxBandErrorEnum> errorManager)
        {
            _errorManager = errorManager;
        }

        public void ValidateSalary(int salary)
        {
            if (salary < 0)
            {
                throw _errorManager.BuildException(TaxBandErrorEnum.InvalidSalaryValue);
            }
        }

        public void ValidateBounds(int lowerBound, int upperBound)
        {
            if (lowerBound >= upperBound)
            {
                throw _errorManager.BuildException(TaxBandErrorEnum.InvalidBounds);
            }
        }
    }
}
