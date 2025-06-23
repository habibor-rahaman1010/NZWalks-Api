using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NZWalks.API.Dtos.RegionsDto
{
    public class RegionAddDto
    {
        [Required, MaxLength(20, ErrorMessage = "Max length would be 20")]
        public string Code { get; set; }

        [Required, MaxLength(50, ErrorMessage = "Max length would be 50")]
        public string Name { get; set; }

        [AllowNull]
        public string? RegionImageUrl { get; set; }
    }
}
