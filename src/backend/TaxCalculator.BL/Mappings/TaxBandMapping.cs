using AutoMapper;
using TaxCalculator.DbModels;
using TaxCalculator.InternalModels;

namespace TaxCalculator.BL.Mappings
{
    public class TaxBandMapping : Profile
    {
        public TaxBandMapping()
        {
            CreateMap<TaxBand, InternalTaxBand>().ReverseMap();
        }
    }
}
