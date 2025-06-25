namespace NZWalks.API.Dtos.DifficultiesDto
{
    public class DifficultyUpdateRequestDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Max length would be 20")]
        public string Name { get; set; } = string.Empty;
    }
}
