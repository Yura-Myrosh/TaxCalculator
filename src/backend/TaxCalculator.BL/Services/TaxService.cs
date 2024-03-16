using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Models;
using TaxCalculator.BL.Validator;
using TaxCalculator.DbModels;

namespace TaxCalculator.BL.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxBandCRUDService _taxBandCRUDService;
        private readonly ITaxBandValidator _taxBandValidator;

        public TaxService(ITaxBandCRUDService taxBandCRUDService, ITaxBandValidator taxBandValidator)
        {
            _taxBandCRUDService = taxBandCRUDService;
            _taxBandValidator = taxBandValidator;
        }

        public async Task<TaxDetails> CalculateTaxes(int salary)
        {
            _taxBandValidator.ValidateSalary(salary);

            var taxBands = await _taxBandCRUDService.GetTaxBandsToCalculateByLowerBoundAsync(salary);
            var taxDetails = new TaxDetails(salary)
            {

                AnnualTaxPaid = GetAnnualTaxPaid(salary, taxBands)
            };

            return taxDetails;
        }

        private decimal GetAnnualTaxPaid(int salary, IEnumerable<TaxBand> taxBands)
        {
            decimal annualTaxPaid = 0;

            foreach (var taxBand in taxBands)
            {
                var boundDiff = taxBand.UpperBound - taxBand.LowerBound;

                var valueToTax = taxBand.UpperBound <= salary ? boundDiff : salary;
                annualTaxPaid += (taxBand.RateInPercents * valueToTax) / 100;

                salary -= boundDiff;
            }

            return annualTaxPaid;
        }
    }
}
