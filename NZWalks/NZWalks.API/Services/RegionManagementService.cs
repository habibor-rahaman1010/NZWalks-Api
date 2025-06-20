using Microsoft.EntityFrameworkCore.Query;
using NZWalks.API.DomainEntities;
using NZWalks.API.ServicesInterface;
using NZWalks.API.UnitOfWorkInterface;
using System.Linq.Expressions;

namespace NZWalks.API.Services
{
    public class RegionManagementService : IRegionManagementService
    {
        private readonly INZWalksUnitOfWork _nZWalksUnitOfWork;

        public RegionManagementService(INZWalksUnitOfWork nZWalksUnitOfWork)
        {
            _nZWalksUnitOfWork = nZWalksUnitOfWork;
        }
        public async Task<(IList<Region> Items, int CurrentPage, int TotalPages, int TotalItems)> GetRegionsAsync(
            int pageIndex, int pageSize,
            Expression<Func<Region, bool>>? filter = null,
            Func<IQueryable<Region>, IIncludableQueryable<Region, object>>? include = null,
            CancellationToken cancellationToken = default)
        {
            return await _nZWalksUnitOfWork.RegionRepository.GetAllAsync(pageIndex, pageSize, filter, include, cancellationToken);
        }

        public async Task AddRegionAsync(Region region)
        {
            await _nZWalksUnitOfWork.RegionRepository.AddAsync(region);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task<Region> GetByIdRegion(Guid id)
        {
            return await _nZWalksUnitOfWork.RegionRepository.GetByIdAsync(id);
        }

        public async Task UpdateRegionAsync(Region region)
        {
            await _nZWalksUnitOfWork.RegionRepository.UpdateAsync(region);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task DeleteRegionAsync(Region region)
        {
            await _nZWalksUnitOfWork.RegionRepository.DeleteAsync(region);
            await _nZWalksUnitOfWork.SaveAsync();
        }
    }
}
