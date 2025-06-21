using Microsoft.EntityFrameworkCore;
using NZWalks.API.DomainEntities;
using NZWalks.API.ServicesInterface;
using NZWalks.API.UnitOfWorkInterface;
using System.Linq.Expressions;

namespace NZWalks.API.Services
{
    public class WalkManagementService : IWalkManagementService
    {
        private readonly INZWalksUnitOfWork _nZWalksUnitOfWork;
        public WalkManagementService(INZWalksUnitOfWork nZWalksUnitOfWork)
        {
            _nZWalksUnitOfWork = nZWalksUnitOfWork;
        }

        public async Task AddWalkAsync(Walk walk)
        {
            await _nZWalksUnitOfWork.WalkRepository.AddAsync(walk);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task<Walk> GetByIdWalkAsync(Guid id)
        {
            return await _nZWalksUnitOfWork.WalkRepository.GetByIdAsync(id, include: q => q
                .Include(w => w.Difficulty)
                .Include(w => w.Region));
        }

        public async Task<(IList<Walk> Items, int CurrentPage, int TotalPages, int TotalItems)> 
                GetWalksAsync(int pageIndex, int pageSize, string? search = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<Walk, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filter = x => x.Name.Contains(search) || x.Description.Contains(search);
            }

            return await _nZWalksUnitOfWork.WalkRepository.GetAllAsync(pageIndex, pageSize, filter,
                include: q => q
                .Include(w => w.Difficulty)
                .Include(w => w.Region), cancellationToken);
        }

        public async Task UpdateWalkAsync(Walk walk)
        {
            await _nZWalksUnitOfWork.WalkRepository.UpdateAsync(walk);
            await _nZWalksUnitOfWork.SaveAsync();
        }

        public async Task DeleteWalkAsync(Walk walk)
        {
            await _nZWalksUnitOfWork.WalkRepository.DeleteAsync(walk);
            await _nZWalksUnitOfWork.SaveAsync();
        }
    }
}
