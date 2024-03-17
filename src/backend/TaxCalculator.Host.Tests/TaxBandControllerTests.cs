using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Moq;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Models;
using TaxCalculator.Common.Models;
using TaxCalculator.Host.Controllers;
using TaxCalculator.InternalModels;

namespace TaxCalculator.Host.Tests
{
    public class TaxBandControllerTests
    {
        private readonly TaxBandController _controller;
        private readonly Mock<ITaxService> _taxServiceMock = new Mock<ITaxService>();
        private readonly Mock<IOutputCacheStore> _cacheStore = new Mock<IOutputCacheStore>();

        public TaxBandControllerTests()
        {
            _controller = new TaxBandController(_taxServiceMock.Object, _cacheStore.Object);
        }

        [Fact]
        public async Task CalculateTaxesBySalary_ReturnsOkResult_WithValidSalary()
        {
            int salary = 50000;
            var taxDetails = new TaxDetails(salary);
            _taxServiceMock.Setup(s => s.CalculateTaxesAsync(salary)).ReturnsAsync(taxDetails);

            var result = await _controller.CalculateTaxesBySalary(salary);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTaxDetails = Assert.IsType<TaxDetails>(okResult.Value);
            Assert.Equal(taxDetails, returnedTaxDetails);
            _taxServiceMock.Verify(s => s.CalculateTaxesAsync(salary), Times.Once);
        }

        [Fact]
        public async Task TaxBandRead_ReturnsOkResult_WithValidId()
        {
            var id = Guid.NewGuid();
            var internalTaxBand = new InternalTaxBand();
            _taxServiceMock.Setup(s => s.GetTaxBandAsync(id)).ReturnsAsync(internalTaxBand);

            var result = await _controller.TaxBandRead(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTaxBand = Assert.IsType<InternalTaxBand>(okResult.Value);
            Assert.Equal(internalTaxBand, returnedTaxBand);
            _taxServiceMock.Verify(s => s.GetTaxBandAsync(id), Times.Once);
        }

        [Fact]
        public async Task TaxBandUpdate_ReturnsOkResult_WithValidData()
        {
            var id = Guid.NewGuid();
            var taxBandToUpdate = new InternalTaxBand();
            _taxServiceMock.Setup(s => s.UpdateTaxBandAsync(id, taxBandToUpdate)).ReturnsAsync(taxBandToUpdate);

            var result = await _controller.TaxBandUpdate(id, taxBandToUpdate, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTaxBand = Assert.IsType<InternalTaxBand>(okResult.Value);
            Assert.Equal(taxBandToUpdate, returnedTaxBand);
            _taxServiceMock.Verify(s => s.UpdateTaxBandAsync(id, taxBandToUpdate), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.SALARY_TAG, It.IsAny<CancellationToken>()), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.TAXBAND_TAG, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TaxBandRemove_ReturnsOkResult_WithValidId()
        {
            var id = Guid.NewGuid();

            var result = await _controller.TaxBandRemove(id, CancellationToken.None);

            Assert.IsType<OkResult>(result);
            _taxServiceMock.Verify(s => s.RemoveTaxBandAsync(id), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.SALARY_TAG, It.IsAny<CancellationToken>()), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.TAXBAND_TAG, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TaxBandCreate_ReturnsOkResult_WithValidTaxBand()
        {
            var taxBandToCreate = new InternalTaxBand();
            _taxServiceMock.Setup(s => s.CreateTaxBandAsync(taxBandToCreate)).ReturnsAsync(taxBandToCreate);

            var result = await _controller.TaxBandCreate(taxBandToCreate, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTaxBand = Assert.IsType<InternalTaxBand>(okResult.Value);
            Assert.Equal(taxBandToCreate, returnedTaxBand);
            _taxServiceMock.Verify(s => s.CreateTaxBandAsync(taxBandToCreate), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.SALARY_TAG, It.IsAny<CancellationToken>()), Times.Once);
            _cacheStore.Verify(c => c.EvictByTagAsync(Constants.TAXBAND_TAG, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TaxBandList_ReturnsOkResult_WithTaxBands()
        {
            var taxBands = new List<InternalTaxBand>
            {
                new InternalTaxBand(),
                new InternalTaxBand()
            };

            _taxServiceMock.Setup(s => s.GetAllTaxBandsAsync()).ReturnsAsync(taxBands);

            var result = await _controller.TaxBandList();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTaxBands = Assert.IsType<List<InternalTaxBand>>(okResult.Value);
            Assert.Equal(taxBands, returnedTaxBands);
            _taxServiceMock.Verify(s => s.GetAllTaxBandsAsync(), Times.Once);
        }
    }
}