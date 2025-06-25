namespace NZWalks.API.Repositories
{
    public class DifficultyRepository : Repository<Difficulty, Guid>, IDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public DifficultyRepository(NZWalksDbContext dbcontext) : base(dbcontext)
        {
            _nZWalksDbContext = dbcontext;
        }
    }
}
