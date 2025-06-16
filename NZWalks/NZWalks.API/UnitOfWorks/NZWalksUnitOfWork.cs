using NZWalks.API.Data;
using NZWalks.API.RepositoriesInterface;
using NZWalks.API.UnitOfWorkInterface;

namespace NZWalks.API.UnitOfWorks
{
    public class NZWalksUnitOfWork : UnitOfWork, INZWalksUnitOfWork
    {
        public IRegionRepository RegionRepository { get; private set; }

        public NZWalksUnitOfWork(NZWalksDbContext dbContext, 
            IRegionRepository regionRepository) : base(dbContext)
        {
            RegionRepository = regionRepository;
        }
    }
}
