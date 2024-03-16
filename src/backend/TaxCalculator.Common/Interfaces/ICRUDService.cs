using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Common.Interfaces
{
    public interface ICRUDService<T>
    {
        Task<IEnumerable<T>> ReadItems();
        Task<T> ReadItemById(Guid id);
        Task CreateAsync(T item);
        void Delete(T item);
        Task DeleteByIdAsync(Guid id);
        void Update(T model);
    }
}
