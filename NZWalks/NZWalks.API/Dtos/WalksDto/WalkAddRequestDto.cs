namespace NZWalks.API.Dtos.WalksDto
{
    public class WalkAddRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficulyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
