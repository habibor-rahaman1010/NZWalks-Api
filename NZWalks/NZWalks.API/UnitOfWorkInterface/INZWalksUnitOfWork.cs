using NZWalks.API.RepositoriesInterface;

namespace NZWalks.API.UnitOfWorkInterface
{
    public interface INZWalksUnitOfWork : IUnitOfWork
    {
        public IRegionRepository RegionRepository { get; }
    }
}
