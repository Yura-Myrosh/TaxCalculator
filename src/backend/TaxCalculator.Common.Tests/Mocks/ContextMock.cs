using Microsoft.EntityFrameworkCore;
using TaxCalculator.DAL.Context;

namespace TaxCalculator.Common.Tests.Mocks
{
    public static class ContextMock
    {
        public static TaxCalculatorDbContext GetMockedContext(string testCollectionName)
        {
            var options = new DbContextOptionsBuilder<TaxCalculatorDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_From:{testCollectionName}")
                .Options;

            return new TaxCalculatorDbContext(options);
        }
    }
}
