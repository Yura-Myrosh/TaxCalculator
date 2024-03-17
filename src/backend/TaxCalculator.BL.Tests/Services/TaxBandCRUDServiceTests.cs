using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Services;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Models;
using TaxCalculator.Common.Tests.Mocks;
using TaxCalculator.DAL.Implementation;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.BL.Tests.Services
{
    public class TaxBandCRUDServiceTests
    {
        private readonly ITaxBandCRUDService _taxBandCRUDService;
        private readonly IUnitOfWork _unitOfWork;

        public TaxBandCRUDServiceTests()
        {
            var context = ContextMock.GetMockedContext(nameof(TaxBandCRUDServiceTests));
            var validator = new CommonValidator<TaxBand>(new ErrorManager<CommonErrorEnum>());
            _unitOfWork = new UnitOfWork(context);
            _taxBandCRUDService = new TaxBandCRUDService(_unitOfWork, validator);
        }

        [Fact]
        public async Task GetTaxBandsToCalculateByLowerBoundAsync_ReturnsCorrectTaxBands()
        {
            await ResetDb();
            await _taxBandCRUDService.CreateAsync(new TaxBand { LowerBound = 0, UpperBound = 4000, RateInPercents = 5 });
            await _taxBandCRUDService.CreateAsync(new TaxBand { LowerBound = 4000, UpperBound = 10000, RateInPercents = 10 });
            await _taxBandCRUDService.CreateAsync(new TaxBand { LowerBound = 10000, UpperBound = 20000, RateInPercents = 15 });

            var taxBands = await _taxBandCRUDService.GetTaxBandsToCalculateByLowerBoundAsync(5000);

            Assert.Equal(2, taxBands.Count());
            Assert.All(taxBands, tb => Assert.True(tb.LowerBound < 5000));
        }

        [Fact]
        public async Task GetTaxBandsToCalculateByLowerBoundAsync_ReturnsEmptyWhenNoMatchingBands()
        {
            await ResetDb();
            await _taxBandCRUDService.CreateAsync(new TaxBand { LowerBound = 20000, UpperBound = 50000, RateInPercents = 50 });
            await _taxBandCRUDService.CreateAsync(new TaxBand { LowerBound = 5000, UpperBound = 10000, RateInPercents = 10 });

            await Assert.ThrowsAsync<ApiException>(() => _taxBandCRUDService.GetTaxBandsToCalculateByLowerBoundAsync(10));
        }

        [Fact]
        public async Task GetTaxBandsToCalculateByLowerBoundAsync_ThrowsValidationExceptionForInvalidLowerBound()
        {
            await ResetDb();
            await Assert.ThrowsAsync<ApiException>(() => _taxBandCRUDService.GetTaxBandsToCalculateByLowerBoundAsync(-1));
        }

        private async Task ResetDb()
        {
            var allItems = await _unitOfWork.TaxBandRepository.GetItemsAsync();

            foreach (var item in allItems)
            {
                _unitOfWork.TaxBandRepository.Delete(item);
            }
        }
    }
}
