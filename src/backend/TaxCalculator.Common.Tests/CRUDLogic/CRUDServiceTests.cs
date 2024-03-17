using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Models;
using TaxCalculator.Common.Tests.Mocks;
using TaxCalculator.DAL.Implementation;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.Common.Tests.CRUDLogic
{
    public class CRUDServiceTests
    {
        private readonly ICRUDService<TaxBand> _crudService;
        private readonly ITaxBandRepository _taxBandRepository;

        public CRUDServiceTests()
        {
            var context = ContextMock.GetMockedContext(nameof(CRUDServiceTests));
            _taxBandRepository = new TaxBandRepository(context);
            var validator = new CommonValidator<TaxBand>(new ErrorManager<CommonErrorEnum>());

            _crudService = new CRUDService<TaxBand>(_taxBandRepository, validator);
        }

        [Fact]
        public async Task CreateAsync_AddsItemSuccessfully()
        {
            await ResetDb();
            var newItem = new TaxBand { LowerBound = 0, UpperBound = 10000, RateInPercents = 10 };
            await _crudService.CreateAsync(newItem);

            var items = await _crudService.ReadItemsAsync();
            Assert.Contains(items, item => item.LowerBound == newItem.LowerBound && item.UpperBound == newItem.UpperBound);
        }

        [Fact]
        public async Task DeleteAsync_RemovesItemSuccessfully()
        {
            await ResetDb();
            var newItem = new TaxBand { LowerBound = 10000, UpperBound = 20000, RateInPercents = 15 };
            await _crudService.CreateAsync(newItem);

            var itemsBeforeDelete = await _crudService.ReadItemsAsync();
            var itemToDelete = itemsBeforeDelete.Last();
            await _crudService.DeleteAsync(itemToDelete);

            await Assert.ThrowsAsync<ApiException>(() => _crudService.ReadItemsAsync());
        }

        [Fact]
        public async Task ReadItemByIdAsync_ReturnsCorrectItem()
        {
            await ResetDb();
            var newItem = new TaxBand { LowerBound = 20000, UpperBound = 30000, RateInPercents = 20 };
            await _crudService.CreateAsync(newItem);

            var items = await _crudService.ReadItemsAsync();
            var itemToRead = items.First();
            var readItem = await _crudService.ReadItemByIdAsync(itemToRead.Id);

            Assert.Equal(itemToRead.Id, readItem.Id);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesItemSuccessfully()
        {
            await ResetDb();
            var newItem = new TaxBand { LowerBound = 30000, UpperBound = 40000, RateInPercents = 25 };
            await _crudService.CreateAsync(newItem);

            var items = await _crudService.ReadItemsAsync();
            var itemToUpdate = items.First();
            itemToUpdate.RateInPercents = 30;
            await _crudService.UpdateAsync(itemToUpdate);

            var updatedItem = await _crudService.ReadItemByIdAsync(itemToUpdate.Id);
            Assert.Equal(30, updatedItem.RateInPercents);
        }

        [Fact]
        public async Task DeleteByIdAsync_RemovesItemSuccessfully()
        {
            await ResetDb();
            var newItem = new TaxBand { LowerBound = 40000, UpperBound = 50000, RateInPercents = 30 };
            await _crudService.CreateAsync(newItem);

            var itemsBeforeDelete = await _crudService.ReadItemsAsync();
            var itemToDelete = itemsBeforeDelete.Last();
            await _crudService.DeleteByIdAsync(itemToDelete.Id);

            await Assert.ThrowsAsync<ApiException>(() => _crudService.ReadItemsAsync());
        }

        [Fact]
        public async Task ReadItemsAsync_ReturnsAllItems()
        {
            await ResetDb();
            await _crudService.CreateAsync(new TaxBand { LowerBound = 0, UpperBound = 5000, RateInPercents = 5 });
            await _crudService.CreateAsync(new TaxBand { LowerBound = 5000, UpperBound = 10000, RateInPercents = 10 });

            var items = await _crudService.ReadItemsAsync();
            Assert.Equal(2, items.Count());
        }

        private async Task ResetDb()
        {
            var allItems = await _taxBandRepository.GetItemsAsync();

            foreach (var item in allItems)
            {
                _taxBandRepository.Delete(item);
            }
        }
    }
}
