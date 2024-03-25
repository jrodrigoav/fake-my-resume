using System.Text.Json.Serialization;

namespace MakeMyResume.DTOs
{
    public class WorkExperienceDTO
    {
        [JsonPropertyName("fromYear")]
        public int FromYear { get; set; }

        [JsonPropertyName("fromMonth")]
        public int FromMonth { get; set; }

        [JsonPropertyName("toYear")]
        public int ToYear { get; set; }

        [JsonPropertyName("toMonth")]
        public int ToMonth { get; set; }

        [JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }

        [JsonPropertyName("projectName")]
        public string? ProjectName { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("projects")]
        public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
    }
}
