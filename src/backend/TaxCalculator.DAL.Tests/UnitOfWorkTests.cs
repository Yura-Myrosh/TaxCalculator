using TaxCalculator.Common.Tests.Mocks;
using TaxCalculator.DAL.Implementation;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.DAL.Tests
{
    public class UnitOfWorkTests
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var context = ContextMock.GetMockedContext(nameof(UnitOfWorkTests));
            _unitOfWork = new UnitOfWork(context);
        }

        [Fact]
        public void TaxBandRepository_Initialization_NotNull()
        {
            var repository = _unitOfWork.TaxBandRepository;

            Assert.NotNull(repository);
        }

        [Fact]
        public async Task SaveAsync_CreateThenSave_ItemPersisted()
        {
            var itemToAdd = new TaxBand() { LowerBound = 0, UpperBound = 10, RateInPercents = 0 };

            await _unitOfWork.TaxBandRepository.CreateAsync(itemToAdd);

            var allItems = await _unitOfWork.TaxBandRepository.GetItemsAsync();

            Assert.Empty(allItems);

            await _unitOfWork.SaveAsync();
            allItems = await _unitOfWork.TaxBandRepository.GetItemsAsync();

            Assert.NotEmpty(allItems);
        }
    }
}
