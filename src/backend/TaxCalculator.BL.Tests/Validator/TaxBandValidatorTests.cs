using System.Net;
using TaxCalculator.BL.Validator;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Models;

namespace TaxCalculator.BL.Tests
{
    public class TaxBandValidatorTests
    {
        private readonly ITaxBandValidator _taxBandValidator;

        public TaxBandValidatorTests()
        {
            _taxBandValidator = new TaxBandValidator(new ErrorManager<TaxBandErrorEnum>());
        }

        [Fact]
        public void ValidateSalary_WithValidSalary_DoesNotThrowException()
        {
            int validSalary = 5000;

            var exception = Record.Exception(() => _taxBandValidator.ValidateSalary(validSalary));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateSalary_WithInvalidSalary_ThrowsApiException()
        {
            int invalidSalary = -1;

            var exception = Assert.Throws<ApiException>(() => _taxBandValidator.ValidateSalary(invalidSalary));

            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Contains("Salary couldn't be less than zero", exception.Message);
            Assert.Contains("InvalidSalaryValue", exception.Error.Code);
        }

        [Fact]
        public void ValidateBounds_WithValidBounds_DoesNotThrowException()
        {
            int lowerBound = 0;
            int upperBound = 100;

            var exception = Record.Exception(() => _taxBandValidator.ValidateBounds(lowerBound, upperBound));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateBounds_WithInvalidBounds_ThrowsApiException()
        {
            int lowerBound = 100;
            int upperBound = 100;

            var exception = Assert.Throws<ApiException>(() => _taxBandValidator.ValidateBounds(lowerBound, upperBound));

            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Contains("Upper bound couldn't be less than lower.", exception.Message);
        }
    }
}
