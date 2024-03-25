using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MakeMyResume.DTOs
{
    public class CreateResumeDTO
    {

        [JsonPropertyName("accountId")]
        public string? AccountId { get; set; }

        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("currentRole")]
        public string? CurrentRole { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("technologies")]
        public List<string>? Technologies { get; set; }

        [JsonPropertyName("methodologies")]
        public List<string>? Methodologies { get; set; }

        [JsonPropertyName("certifications")]
        public List<string> Certifications { get; set; } = new List<string> { };

        [JsonPropertyName("workExperience")]
        public List<WorkExperienceDTO>? WorkExperience { get; set; }

        [JsonPropertyName("education")]
        public List<EducationDTO>? Education { get; set; }
    }
}
