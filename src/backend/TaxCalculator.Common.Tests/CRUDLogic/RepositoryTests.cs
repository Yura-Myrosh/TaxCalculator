using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Tests.Mocks;
using TaxCalculator.DAL.Context;
using TaxCalculator.DbModels;

namespace TaxCalculator.Common.Tests.CRUDLogic
{
    public class RepositoryTests
    {
        private readonly IRepository<TaxBand> _repository;

        public RepositoryTests()
        {
            var context = ContextMock.GetMockedContext(nameof(RepositoryTests));
            _repository = new Repository<TaxBand, TaxCalculatorDbContext>(context);
        }

        [Fact]
        public async Task GetItemsAsync_ReturnsAllItems()
        {
            var newItem = new TaxBand { LowerBound = 0, UpperBound = 10000, RateInPercents = 10 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var items = await _repository.GetItemsAsync();
            Assert.Contains(items, item => item.LowerBound == newItem.LowerBound && item.UpperBound == newItem.UpperBound);
        }

        [Fact]
        public async Task CreateAsync_AddsNewItem()
        {
            var newItem = new TaxBand { LowerBound = 10000, UpperBound = 20000, RateInPercents = 15 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var items = await _repository.GetItemsAsync();
            Assert.Contains(items, item => item.LowerBound == newItem.LowerBound && item.UpperBound == newItem.UpperBound);
        }

        [Fact]
        public async Task Delete_RemovesItem()
        {
            var newItem = new TaxBand { LowerBound = 22222, UpperBound = 33333, RateInPercents = 22 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var itemToDelete = (await _repository.GetItemsAsync()).Last();
            _repository.Delete(itemToDelete);
            await _repository.SaveAsync();

            var items = await _repository.GetItemsAsync();
            Assert.DoesNotContain(items, item => item.LowerBound == newItem.LowerBound && item.UpperBound == newItem.UpperBound);
        }

        [Fact]
        public async Task DeleteByIdAsync_RemovesItemById()
        {
            var newItem = new TaxBand { LowerBound = 30000, UpperBound = 40000, RateInPercents = 25 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var itemToDelete = await _repository.GetItemsAsync();
            await _repository.DeleteByIdAsync(itemToDelete.Last().Id);
            await _repository.SaveAsync();

            var items = await _repository.GetItemsAsync();
            Assert.DoesNotContain(items, item => item.LowerBound == newItem.LowerBound && item.UpperBound == newItem.UpperBound);
        }

        [Fact]
        public async Task GetItemAsync_ReturnsItemById()
        {
            var newItem = new TaxBand { LowerBound = 40000, UpperBound = 50000, RateInPercents = 30 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var itemToGet = await _repository.GetItemsAsync();
            var fetchedItem = await _repository.GetItemAsync(itemToGet.First().Id);

            Assert.Equal(fetchedItem.Id, itemToGet.First().Id);
        }

        [Fact]
        public async Task Update_UpdatesItem()
        {
            var newItem = new TaxBand { LowerBound = 50000, UpperBound = 60000, RateInPercents = 35 };
            await _repository.CreateAsync(newItem);
            await _repository.SaveAsync();

            var itemToUpdate = await _repository.GetItemsAsync();
            var updateItem = itemToUpdate.First();
            updateItem.RateInPercents = 40;
            _repository.Update(updateItem);
            await _repository.SaveAsync();

            var updatedItem = await _repository.GetItemAsync(updateItem.Id);
            Assert.Equal(40, updatedItem.RateInPercents);
        }
    }
}
