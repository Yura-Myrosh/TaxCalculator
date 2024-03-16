using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Common.Interfaces;

namespace TaxCalculator.Common.Implementation
{
    public class CRUDService<T> : ICRUDService<T>
    {
        private readonly IRepository<T> _repository;
        private readonly ICommonValidator<T> _validator;

        public CRUDService(IRepository<T> repository, ICommonValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public Task CreateAsync(T item)
        {
            _validator.ValidateItem(item);
            return _repository.CreateAsync(item);
        }

        public void Delete(T item)
        {
            _validator.ValidateItem(item);
            _repository.Delete(item);
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return _repository.DeleteByIdAsync(id);
        }

        public async Task<T> ReadItemById(Guid id)
        {
            var result = await _repository.GetItemAsync(id);

            _validator.ValidateItem(result);

            return result;
        }

        public async Task<IEnumerable<T>> ReadItems()
        {
            var result = await _repository.GetItemsAsync();

            _validator.ValidateReadItems(result);

            return result;
        }

        public void Update(T model)
        {
            _validator.ValidateItem(model);
            _repository.Update(model);
        }
    }
}
