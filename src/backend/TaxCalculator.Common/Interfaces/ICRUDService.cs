using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Common.Interfaces
{
    public interface ICRUDService<T>
    {
        Task<IEnumerable<T>> ReadItemsAsync();
        Task<T> ReadItemByIdAsync(Guid id);
        Task CreateAsync(T item);
        Task DeleteAsync(T item);
        Task DeleteByIdAsync(Guid id);
        Task UpdateAsync(T model);
    }
}
