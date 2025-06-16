using NZWalks.API.DomainEntities;

namespace NZWalks.API.ServicesInterface
{
    public interface IRegionManagementService
    {
        public Task AddRegionAsync(Region region);
    }
}
