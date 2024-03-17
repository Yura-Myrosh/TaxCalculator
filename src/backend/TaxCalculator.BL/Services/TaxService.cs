using AutoMapper;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Models;
using TaxCalculator.BL.Validator;
using TaxCalculator.DbModels;
using TaxCalculator.InternalModels;

namespace TaxCalculator.BL.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxBandCRUDService _taxBandCRUDService;
        private readonly ITaxBandValidator _taxBandValidator;
        private readonly IMapper _mapper;

        public TaxService(ITaxBandCRUDService taxBandCRUDService, ITaxBandValidator taxBandValidator, IMapper mapper)
        {
            _taxBandCRUDService = taxBandCRUDService;
            _taxBandValidator = taxBandValidator;
            _mapper = mapper;
        }

        public async Task<TaxDetails> CalculateTaxesAsync(int salary)
        {
            _taxBandValidator.ValidateSalary(salary);

            var taxBands = await _taxBandCRUDService.GetTaxBandsToCalculateByLowerBoundAsync(salary);
            var taxDetails = new TaxDetails(salary)
            {

                AnnualTaxPaid = GetAnnualTaxPaid(salary, taxBands)
            };

            return taxDetails;
        }

        public async Task<InternalTaxBand> GetTaxBandAsync(Guid id)
        {
            var taxBand = await _taxBandCRUDService.ReadItemByIdAsync(id);

            return _mapper.Map<InternalTaxBand>(taxBand);
        }


        public async Task<IEnumerable<InternalTaxBand>> GetAllTaxBandsAsync()
        {
            var taxBands = await _taxBandCRUDService.ReadItemsAsync();

            return _mapper.Map<IEnumerable<InternalTaxBand>>(taxBands);
        }

        public async Task<InternalTaxBand> CreateTaxBandAsync(InternalTaxBand taxBand)
        {
            var itemToCreate = _mapper.Map<TaxBand>(taxBand);

            await _taxBandCRUDService.CreateAsync(itemToCreate);

            return taxBand;
        }

        public async Task<InternalTaxBand> UpdateTaxBandAsync(Guid id, InternalTaxBand taxBand)
        {
            taxBand.Id = id;
            var itemToUpdate = _mapper.Map<TaxBand>(taxBand);

            await _taxBandCRUDService.UpdateAsync(itemToUpdate);

            return taxBand;
        }

        public async Task<InternalTaxBand> RemoveTaxBandAsync(InternalTaxBand taxBand)
        {
            var itemToRemove = _mapper.Map<TaxBand>(taxBand);

            await _taxBandCRUDService.DeleteAsync(itemToRemove);

            return taxBand;
        }

        public async Task RemoveTaxBandAsync(Guid id)
        {
            await _taxBandCRUDService.DeleteByIdAsync(id);
        }

        private static decimal GetAnnualTaxPaid(int salary, IEnumerable<TaxBand> taxBands)
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
