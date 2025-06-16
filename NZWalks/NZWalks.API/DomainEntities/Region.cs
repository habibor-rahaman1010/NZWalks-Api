namespace NZWalks.API.DomainEntities
{
    public class Region : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
