using Microsoft.EntityFrameworkCore;
using TaxCalculator.DbModels;

namespace TaxCalculator.DAL.Context
{
    public class TaxCalculatorDbContext : DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options)
        {
        }

        DbSet<TaxBand> TaxBands { get; set; }
    }
}
