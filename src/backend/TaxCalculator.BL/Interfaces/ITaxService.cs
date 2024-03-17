using TaxCalculator.BL.Models;
using TaxCalculator.InternalModels;

namespace TaxCalculator.BL.Interfaces
{
    public interface ITaxService
    {
        public Task<TaxDetails> CalculateTaxesAsync(int salary);
        Task<InternalTaxBand> CreateTaxBandAsync(InternalTaxBand taxBand);
        Task<IEnumerable<InternalTaxBand>> GetAllTaxBandsAsync();
        Task<InternalTaxBand> GetTaxBandAsync(Guid id);
        Task RemoveTaxBandAsync(Guid id);
        Task<InternalTaxBand> RemoveTaxBandAsync(InternalTaxBand taxBand);
        Task<InternalTaxBand> UpdateTaxBandAsync(Guid id, InternalTaxBand taxBand);
    }
}
