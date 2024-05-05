using System.Text.Json.Serialization;

namespace FakeMyResume.Models.StackExchange
{
    public class Tag    
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("has_synonyms")]
        public bool HasSynonyms { get; init; }

        [JsonPropertyName("is_moderator_only")]
        public bool IsModeratorOnly { get; init; }

        [JsonPropertyName("is_required")]
        public bool IsRequired { get; init; }

        [JsonPropertyName("count")]
        public int Count { get; init; }        
    }
}
