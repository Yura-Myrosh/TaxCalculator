namespace TaxCalculator.Common.Interfaces
{
    public interface ICommonValidator<T>
    {
        void ValidateItem(T entity);
        void ValidateReadItems(IEnumerable<T> entities);
    }
}
