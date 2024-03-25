using System.Text.Json.Serialization;

namespace MakeMyResume.DTOs
{
    public class ProjectDTO
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("technologiesUsed")]
        public List<string> TechnologiesUsed { get; set; } = new List<string>();
    }
}
