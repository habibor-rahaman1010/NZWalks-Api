namespace NZWalks.API.Services
{
    public class DifficultyManagementService : IDifficultyManagementService
    {
        private readonly INZWalksUnitOfWork _nZWalksUnitOfWork;

        public DifficultyManagementService(INZWalksUnitOfWork nZWalksUnitOfWork)
        {
            _nZWalksUnitOfWork = nZWalksUnitOfWork;
        }

        public Task<(IList<Difficulty> Items, int CurrentPage, int TotalPages, int TotalItems)> GetDifficultiesAsync(
            int pageIndex, int pageSize, string? search = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<Difficulty, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = x => x.Name.Contains(search);
            }
            return _nZWalksUnitOfWork.DifficultyRepository.GetAllAsync(pageIndex, pageSize, filter);
        }

        public async Task<Difficulty> GetByIdDifficultyAsync(Guid id)
        {
            return await _nZWalksUnitOfWork.DifficultyRepository.GetByIdAsync(id);
        }

        public async Task AddDifficultyAsync(Difficulty difficulty)
        {
            await _nZWalksUnitOfWork.DifficultyRepository.AddAsync(difficulty);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task UpdateDifficultyAsync(Difficulty difficulty)
        {
            await _nZWalksUnitOfWork.DifficultyRepository.UpdateAsync(difficulty);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task DeleteDifficultyAsync(Difficulty difficulty)
        {
            await _nZWalksUnitOfWork.DifficultyRepository.DeleteAsync(difficulty);
            await _nZWalksUnitOfWork.SaveAsync();
        }
    }
}