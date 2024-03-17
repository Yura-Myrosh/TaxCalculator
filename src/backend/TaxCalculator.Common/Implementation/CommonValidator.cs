using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Common.Implementation
{
    public class CommonValidator<T> : ICommonValidator<T>
    {
        private readonly IErrorManager<CommonErrorEnum> _errorManager;

        public CommonValidator(IErrorManager<CommonErrorEnum> errorManager)
        {
            _errorManager = errorManager;
        }

        public void ValidateId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw _errorManager.BuildException(CommonErrorEnum.EmptyResourceId);
            };
        }

        public void ValidateItemAfterRead(T? result)
        {
            if (result == null)
            {
                throw _errorManager.BuildException(CommonErrorEnum.ItemNotFound);
            }
        }

        public void ValidateItemBeforeRemove(T? item)
        {
            if (item == null)
            {
                throw _errorManager.BuildException(CommonErrorEnum.RemoveUnsuccessful);
            }
        }

        public void ValidateItemBeforeWrite(T? item)
        {
            if (item == null)
            {
                throw _errorManager.BuildException(CommonErrorEnum.ItemNull);
            }
        }

        public void ValidateItemsAfterRead(IEnumerable<T> items)
        {
            if (items == null || !items.Any())
            {
                throw _errorManager.BuildException(CommonErrorEnum.NoItems);
            }
        }
    }
}
