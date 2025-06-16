using NZWalks.API.Data;
using NZWalks.API.DomainEntities;
using NZWalks.API.RepositoriesInterface;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : Repository<Region, Guid>, IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext dbContext) : base(dbContext)
        {
            _nZWalksDbContext = dbContext;
        }
    }
}
