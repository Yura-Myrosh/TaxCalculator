using Microsoft.EntityFrameworkCore;
using TaxCalculator.Common.Implementation;
using TaxCalculator.DAL.Context;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.DAL.Implementation
{
    public class TaxBandRepository : Repository<TaxBand, TaxCalculatorDbContext>, ITaxBandRepository
    {
        private readonly TaxCalculatorDbContext _context;

        public TaxBandRepository(TaxCalculatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxBand>> GetTaxBandsToCalculateByLowerBoundAsync(int valueToCompare)
        {
            return await _context.Set<TaxBand>().Where(x => valueToCompare > x.LowerBound).OrderBy(x => x.RateInPercents).ToListAsync();
        }
    }
}
