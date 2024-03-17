using System.Net;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Models;
using TaxCalculator.DbModels;

namespace TaxCalculator.Common.Tests.Validator
{

    public class CommonValidatorTests
    {
        private readonly ICommonValidator<TaxBand> _validator;

        public CommonValidatorTests()
        {
            _validator = new CommonValidator<TaxBand>(new ErrorManager<CommonErrorEnum>());
        }

        [Fact]
        public void ValidateId_ThrowsCorrectException_IfIdIsEmpty()
        {
            var emptyId = Guid.Empty;
            var exception = Record.Exception(() => _validator.ValidateId(emptyId)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal("The ID passed is incorrect.", exception.Message);
        }

        [Fact]
        public void ValidateItemAfterRead_ThrowsCorrectException_IfResultIsNull()
        {
            TaxBand? result = null;
            var exception = Record.Exception(() => _validator.ValidateItemAfterRead(result)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("An item has not beed found.", exception.Message);
        }

        [Fact]
        public void ValidateItemBeforeRemove_ThrowsCorrectException_IfItemIsNull()
        {
            TaxBand? item = null;
            var exception = Record.Exception(() => _validator.ValidateItemBeforeRemove(item)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("An item has not beed found to revome.", exception.Message);
        }

        [Fact]
        public void ValidateItemBeforeWrite_ThrowsCorrectException_IfItemIsNull()
        {
            TaxBand? item = null;
            var exception = Record.Exception(() => _validator.ValidateItemBeforeWrite(item)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal("An item is null.", exception.Message);
        }

        [Fact]
        public void ValidateItemsAfterRead_ThrowsCorrectException_IfItemsAreEmpty()
        {
            var items = new List<TaxBand>();
            var exception = Record.Exception(() => _validator.ValidateItemsAfterRead(items)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("The sequence contains no items.", exception.Message);
        }

        [Fact]
        public void ValidateItemsAfterRead_ThrowsCorrectException_IfItemsAreNull()
        {
            IEnumerable<TaxBand> items = null;
            var exception = Record.Exception(() => _validator.ValidateItemsAfterRead(items)) as ApiException;
            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("The sequence contains no items.", exception.Message);
        }
    }
}
