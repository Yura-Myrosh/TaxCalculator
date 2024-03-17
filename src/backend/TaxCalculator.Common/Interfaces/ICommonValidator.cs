
namespace TaxCalculator.Common.Interfaces
{
    public interface ICommonValidator<T>
    {
        void ValidateId(Guid id);
        void ValidateItemAfterRead(T? result);
        void ValidateItemBeforeRemove(T? item);
        void ValidateItemBeforeWrite(T? item);
        void ValidateItemsAfterRead(IEnumerable<T> items);
    }
}
