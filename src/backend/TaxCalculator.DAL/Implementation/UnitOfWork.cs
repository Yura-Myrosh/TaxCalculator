using TaxCalculator.Common.Interfaces;
using TaxCalculator.DAL.Context;
using TaxCalculator.DAL.Interfaces;

namespace TaxCalculator.DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaxCalculatorDbContext _context;
        private ITaxBandRepository _taxBandRepository;

        public UnitOfWork(TaxCalculatorDbContext context)
        {
            _context = context;
        }

        public ITaxBandRepository TaxBandRepository
        {
            get
            {
                _taxBandRepository ??= new TaxBandRepository(_context);

                return _taxBandRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
