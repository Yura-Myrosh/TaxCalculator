using TaxCalculator.Common.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.BL.Interfaces
{
    public interface ITaxBandCRUDService : ICRUDService<TaxBand>
    {
        public Task<IEnumerable<TaxBand>> GetTaxBandsToCalculateByLowerBoundAsync(int lowerBound);
    }
}
