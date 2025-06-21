namespace NZWalks.API.Dtos.DifficultiesDto
{
    public class DifficultyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string ModifiedDate { get; set; } = string.Empty;
    }
}
