namespace NZWalks.API.Dtos.WalksDto
{
    public class WalkUpdateRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficulyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
