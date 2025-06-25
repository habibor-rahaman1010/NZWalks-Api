namespace NZWalks.API.Repositories
{
    public class WalkRepository : Repository<Walk, Guid>, IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkRepository(NZWalksDbContext dbContext) : base(dbContext)
        {
            _nZWalksDbContext = dbContext;
        }
    }
}
