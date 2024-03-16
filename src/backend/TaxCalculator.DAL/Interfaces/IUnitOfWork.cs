namespace TaxCalculator.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public ITaxBandRepository TaxBandRepository { get; }
        public Task SaveAsync();
    }
}
