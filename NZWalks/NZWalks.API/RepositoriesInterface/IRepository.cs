using Microsoft.EntityFrameworkCore.Query;
using NZWalks.API.DomainEntities;
using System.Linq.Expressions;

namespace NZWalks.API.RepositoriesInterface
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable<TKey>
    {
        public Task<(IList<TEntity> Items, int CurrentPage, int TotalPages, int TotalItems)> 
            GetAllAsync(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            CancellationToken cancellationToken = default);

        public Task<TEntity> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, CancellationToken cancellationToken = default);
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TKey id, CancellationToken cancellationToken = default);
        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
