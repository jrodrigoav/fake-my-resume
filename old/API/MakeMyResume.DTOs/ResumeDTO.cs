using System.Text.Json.Serialization;

namespace MakeMyResume.DTOs
{

    public class ResumeDTO : CreateResumeDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
