using TaxCalculator.Common.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.DAL.Interfaces
{
    public interface ITaxBandRepository : IRepository<TaxBand>
    {
        Task<IEnumerable<TaxBand>> GetTaxBandsToCalculateByLowerBoundAsync(int valueToCompare);
    }
}
