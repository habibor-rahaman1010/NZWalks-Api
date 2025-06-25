namespace NZWalks.API.Dtos.WalksDto
{
    public class WalkAddRequestDto
    {
        [Required, MaxLength(20, ErrorMessage = "Max length would be 20")]
        public string Name { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Max length would be 100")]
        public string Description { get; set; }

        [Required]
        public double LengthInKm { get; set; }

        [AllowNull]
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficulyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
