using Microsoft.EntityFrameworkCore.Query;
using NZWalks.API.DomainEntities;
using System.Linq.Expressions;

namespace NZWalks.API.ServicesInterface
{
    public interface IRegionManagementService
    {
        public Task<(IList<Region> Items, int CurrentPage, int TotalPages, int TotalItems)> GetRegionsAsync(int pageIndex, int pageSize,
            string? search = null,
            CancellationToken cancellationToken = default);

        public Task<Region> GetByIdRegion(Guid id);
        public Task AddRegionAsync(Region region);
        public Task UpdateRegionAsync(Region region);
        public Task DeleteRegionAsync(Region region);
    }
}
