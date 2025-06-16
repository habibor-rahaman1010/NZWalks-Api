using NZWalks.API.Data;
using NZWalks.API.UnitOfWorkInterface;

namespace NZWalks.API.UnitOfWork
{
    public class NZWalksUnitOfWork : UnitOfWork, INZWalksUnitOfWork
    {
        public NZWalksUnitOfWork(NZWalksDbContext dbContext) : base(dbContext)
        {
        }
    }
}
