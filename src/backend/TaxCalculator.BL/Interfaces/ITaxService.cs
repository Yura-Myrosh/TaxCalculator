using TaxCalculator.BL.Models;

namespace TaxCalculator.BL.Interfaces
{
    public interface ITaxService
    {
        public Task<TaxDetails> CalculateTaxes(int salary);
    }
}
