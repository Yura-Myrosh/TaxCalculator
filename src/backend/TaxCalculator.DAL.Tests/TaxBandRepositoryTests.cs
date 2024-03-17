using TaxCalculator.Common.Tests.Mocks;
using TaxCalculator.DAL.Implementation;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.DAL.Tests
{
    public class TaxBandRepositoryTests
    {
        private readonly ITaxBandRepository _repository;

        public TaxBandRepositoryTests()
        {
            var context = ContextMock.GetMockedContext(nameof(TaxBandRepositoryTests));

            _repository = new TaxBandRepository(context);
        }

        [Fact]
        public async Task GetTaxBandsToCalculateByLowerBoundAsync_SalaryWithinBounds_ReturnsCorrectTaxBands()
        {
            await FillDatabaseWithDefaultValues();

            var salary = 10000;

            var results = await _repository.GetTaxBandsToCalculateByLowerBoundAsync(salary);

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public async Task GetTaxBandsToCalculateByLowerBoundAsync_SalaryBelowMinimum_ReturnsEmptyCollection()
        {
            await FillDatabaseWithDefaultValues();

            var salary = -1;

            var results = await _repository.GetTaxBandsToCalculateByLowerBoundAsync(salary);

            Assert.NotNull(results);
            Assert.Empty(results);
        }

        private async Task FillDatabaseWithDefaultValues()
        {
            if (!(await _repository.GetItemsAsync()).Any())
            {
                var tasks = new List<Task>
                {
                    _repository.CreateAsync(new TaxBand() { LowerBound = 0, UpperBound = 5000, RateInPercents = 0 }),
                    _repository.CreateAsync(new TaxBand() { LowerBound = 5000, UpperBound = 20000, RateInPercents = 20 }),
                    _repository.CreateAsync(new TaxBand() { LowerBound = 20000, UpperBound = int.MaxValue, RateInPercents = 40 }),
                    _repository.SaveAsync()
                };

                await Task.WhenAll(tasks);
            }
        }
    }
}