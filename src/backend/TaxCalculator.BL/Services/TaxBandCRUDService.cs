using TaxCalculator.DAL.Interfaces;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.DbModels;
using TaxCalculator.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.BL.Services
{
    public class TaxBandCRUDService : CRUDService<TaxBand>, ITaxBandCRUDService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonValidator<TaxBand> _validator;

        public TaxBandCRUDService(IUnitOfWork unitOfWork, ICommonValidator<TaxBand> validator) : base(unitOfWork.TaxBandRepository, validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<IEnumerable<TaxBand>> GetTaxBandsToCalculateByLowerBoundAsync(int lowerBound)
        {
            var taxBands = await _unitOfWork.TaxBandRepository.GetTaxBandsToCalculateByLowerBoundAsync(lowerBound);

            _validator.ValidateItemsAfterRead(taxBands);

            return taxBands;
        }
    }
}
