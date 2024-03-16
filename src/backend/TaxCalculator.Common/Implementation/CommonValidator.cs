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

        public void ValidateItem(T entity)
        {
            if (entity == null)
            {
                throw _errorManager.BuildException(CommonErrorEnum.ItemNull);
            }
        }

        public void ValidateReadItems(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw _errorManager.BuildException(CommonErrorEnum.NoItems);
            }
        }
    }
}
