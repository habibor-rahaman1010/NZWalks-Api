namespace NZWalks.API.UnitOfWorkInterface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public void Save();
        public Task SaveAsync();
    }
}
