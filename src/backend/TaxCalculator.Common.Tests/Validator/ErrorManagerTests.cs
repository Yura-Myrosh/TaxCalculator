using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Common.Tests.Validator
{
    public class ErrorManagerTests
    {
        private readonly ErrorManager<CommonErrorEnum> _errorManager;

        public ErrorManagerTests()
        {
            _errorManager = new ErrorManager<CommonErrorEnum>();
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForItemNull()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.ItemNull);

            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal("ItemNull", exception.Error.Code);
            Assert.Equal("An item is null.", exception.Message);
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForNoRates()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.NoRates);

            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal("NoRates", exception.Error.Code);
            Assert.Equal("The rates to calculate.", exception.Message);
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForEmptyResourceId()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.EmptyResourceId);

            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal("EmptyResourceId", exception.Error.Code);
            Assert.Equal("The ID passed is incorrect.", exception.Message);
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForNoItems()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.NoItems);

            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("NoItems", exception.Error.Code);
            Assert.Equal("The sequence contains no items.", exception.Message);
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForItemNotFound()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.ItemNotFound);

            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("ItemNotFound", exception.Error.Code);
            Assert.Equal("An item has not beed found.", exception.Message);
        }

        [Fact]
        public void BuildException_ReturnsCorrectApiException_ForRemoveUnsuccessful()
        {
            var exception = _errorManager.BuildException(CommonErrorEnum.RemoveUnsuccessful);

            Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
            Assert.Equal("RemoveUnsuccessful", exception.Error.Code);
            Assert.Equal("An item has not beed found to revome.", exception.Message);
        }
    }
}
