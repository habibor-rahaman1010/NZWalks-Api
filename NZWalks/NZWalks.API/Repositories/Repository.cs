using Microsoft.EntityFrameworkCore;
using NZWalks.API.DomainEntities;
using NZWalks.API.RepositoriesInterface;

namespace NZWalks.API.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();

            var entityToDelete = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (entityToDelete != null)
            {
                await DeleteAsync(entityToDelete, cancellationToken);
            }
        }

        public virtual async Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }, cancellationToken);
        }

        public virtual async Task<(IList<TEntity> Items, int CurrentPage, int TotalPages, int TotalItems)> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();

            var totalItems = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, pageIndex, totalPages, totalItems);
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            var item = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (item != null)
            {
                return item;
            }
            return item;
        }

        public virtual async Task UpdateAsync(TKey id, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable<TEntity>();
            var entityToUpdate = await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            if (entityToUpdate != null)
            {
                await UpdateAsync(entityToUpdate, cancellationToken);
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Entry(entity).State = EntityState.Modified;
            }, cancellationToken);
        }
    }
}
