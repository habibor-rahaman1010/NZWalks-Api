using Microsoft.EntityFrameworkCore.Query;
using NZWalks.API.DomainEntities;
using System.Linq.Expressions;

namespace NZWalks.API.ServicesInterface
{
    public interface IDifficultyManagementService
    {
        public Task<(IList<Difficulty> Items, int CurrentPage, int TotalPages, int TotalItems)> GetDifficultiesAsync(
            int pageIndex, int pageSize,
            Expression<Func<Difficulty, bool>>? filter = null,
            Func<IQueryable<Difficulty>, IIncludableQueryable<Difficulty, object>>? include = null,
            CancellationToken cancellationToken = default);

        public Task<Difficulty> GetByIdDifficultyAsync(Guid id);
        public Task AddDifficultyAsync(Difficulty difficulty);
        public Task UpdateDifficultyAsync(Difficulty difficulty);
        public Task DeleteDifficultyAsync(Difficulty difficulty);
    }
}
