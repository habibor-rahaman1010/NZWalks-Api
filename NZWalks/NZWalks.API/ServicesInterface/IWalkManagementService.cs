using NZWalks.API.DomainEntities;

namespace NZWalks.API.ServicesInterface
{
    public interface IWalkManagementService
    {
        public Task<(IList<Walk> Items, int CurrentPage, int TotalPages, int TotalItems)> GetWalksAsync(int pageIndex, int pageSize,
            string? search = null,
            CancellationToken cancellationToken = default);

        public Task<Walk> GetByIdWalkAsync(Guid id);
        public Task AddWalkAsync(Walk walk);
        public Task UpdateWalkAsync(Walk walk);
        public Task DeleteWalkAsync(Walk walk);
    }
}
