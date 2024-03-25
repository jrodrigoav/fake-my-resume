using System.Text.Json.Serialization;

namespace MakeMyResume.DTOs
{
    public class EducationDTO
    {
        [JsonPropertyName("degree")]
        public string? Degree { get; set; }

        [JsonPropertyName("major")]
        public string? Major { get; set; }

        [JsonPropertyName("universityName")]
        public string? UniversityName { get; set; }

        [JsonPropertyName("yearOfCompletion")]
        public int? YearOfCompletion { get; set; }
    }
}
