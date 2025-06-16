using NZWalks.API.DomainEntities;

namespace NZWalks.API.RepositoriesInterface
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable<TKey>
    {
        public Task<(IList<TEntity> Items, int CurrentPage, int TotalPages, int TotalItems)> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TKey id, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
