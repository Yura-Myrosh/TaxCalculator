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
        public async Task CreateAsync(T item)
        {
            _validator.ValidateItemBeforeWrite(item);
            await _repository.CreateAsync(item);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(T item)
        {
            _validator.ValidateItemBeforeRemove(item);
            _repository.Delete(item);
            await _repository.SaveAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _validator.ValidateId(id);
            await _repository.DeleteByIdAsync(id);
            await _repository.SaveAsync();
        }

        public async Task<T> ReadItemByIdAsync(Guid id)
        {
            _validator.ValidateId(id);

            var result = await _repository.GetItemAsync(id);

            _validator.ValidateItemAfterRead(result);

            return result;
        }

        public async Task<IEnumerable<T>> ReadItemsAsync()
        {
            var result = await _repository.GetItemsAsync();

            _validator.ValidateItemsAfterRead(result);

            return result;
        }

        public async Task UpdateAsync(T model)
        {
            _validator.ValidateItemBeforeWrite(model);
            _repository.Update(model);
            await _repository.SaveAsync();
        }
    }
}
