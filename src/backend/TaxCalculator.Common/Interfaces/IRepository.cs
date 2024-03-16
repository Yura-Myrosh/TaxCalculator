namespace TaxCalculator.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T> GetItemAsync(Guid id);
        Task CreateAsync(T item);
        void Delete(T item);
        Task DeleteByIdAsync(Guid id);
        void Update(T model);
        Task SaveAsync();
    }
}
