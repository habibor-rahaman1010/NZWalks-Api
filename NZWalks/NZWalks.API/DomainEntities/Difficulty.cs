namespace NZWalks.API.DomainEntities
{
    public class Difficulty : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
