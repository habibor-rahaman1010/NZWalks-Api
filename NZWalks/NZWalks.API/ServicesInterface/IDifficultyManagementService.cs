using NZWalks.API.DomainEntities;

namespace NZWalks.API.ServicesInterface
{
    public interface IDifficultyManagementService
    {
        public Task<(IList<Difficulty> Items, int CurrentPage, int TotalPages, int TotalItems)> GetDifficultiesAsync(int pageIndex, int pageSize);
        public Task<Difficulty> GetByIdDifficultyAsync(Guid id);
        public Task AddDifficultyAsync(Difficulty difficulty);
        public Task UpdateDifficultyAsync(Difficulty difficulty);
        public Task DeleteDifficultyAsync(Difficulty difficulty);
    }
}
