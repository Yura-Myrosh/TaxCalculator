using Microsoft.EntityFrameworkCore;
using TaxCalculator.Common.Interfaces;

namespace TaxCalculator.Common.Implementation
{
    public class Repository<TResouce, TContext> : IRepository<TResouce>
        where TResouce : class
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TResouce>> GetItemsAsync()
        {
            return await _context.Set<TResouce>().ToListAsync();
        }

        public async Task CreateAsync(TResouce item)
        {
            _context.Attach(item);
            await _context.Set<TResouce>().AddAsync(item);
        }

        public void Delete(TResouce item)
        {
            _context.Set<TResouce>().Remove(item);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Delete(await GetItemAsync(id));
        }

        public async Task<TResouce> GetItemAsync(Guid id)
        {
            return await _context.Set<TResouce>().FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TResouce model)
        {
            _context.Update(model);
        }
    }
}
