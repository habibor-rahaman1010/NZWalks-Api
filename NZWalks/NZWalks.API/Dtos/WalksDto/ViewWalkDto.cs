using NZWalks.API.Dtos.DifficultiesDto;
using NZWalks.API.Dtos.RegionsDto;

namespace NZWalks.API.Dtos.WalksDto
{
    public class ViewWalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public string ModifiedDate { get; set; } = string.Empty;

        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
