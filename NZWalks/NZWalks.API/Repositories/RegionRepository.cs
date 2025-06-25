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
