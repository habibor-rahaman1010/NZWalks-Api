using NZWalks.API.DomainEntities;

namespace NZWalks.API.ServicesInterface
{
    public interface IRegionManagementService
    {
        public Task<(IList<Region> Items, int CurrentPage, int TotalPages, int TotalItems)> GetRegionsAsync(int pageIndex, int pageSize);
        public Task<Region> GetByIdRegion(Guid id);
        public Task AddRegionAsync(Region region);
        public Task UpdateRegionAsync(Region region);
        public Task DeleteRegionAsync(Region region);
    }
}
