using NZWalks.API.DomainEntities;
using NZWalks.API.ServicesInterface;
using NZWalks.API.UnitOfWorkInterface;

namespace NZWalks.API.Services
{
    public class RegionManagementService : IRegionManagementService
    {
        private readonly INZWalksUnitOfWork _nZWalksUnitOfWork;

        public RegionManagementService(INZWalksUnitOfWork nZWalksUnitOfWork)
        {
            _nZWalksUnitOfWork = nZWalksUnitOfWork;
        }

        public async Task AddRegionAsync(Region region)
        {
            await _nZWalksUnitOfWork.RegionRepository.AddAsync(region);
            await _nZWalksUnitOfWork.SaveAsync();
        }
    }
}
