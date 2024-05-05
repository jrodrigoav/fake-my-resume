using System.Text.Json.Serialization;

namespace FakeMyResume.Models.StackExchange
{
    public class TagsResponse
    {
        [JsonPropertyName("items")]
        public Tag[] Items { get; init; } = Array.Empty<Tag>();
        
        [JsonPropertyName("has_more")]
        public bool HasMore { get; init; }

        [JsonPropertyName("quota_max")]
        public int QuotaMax { get; init; }

        [JsonPropertyName("quota_remaining")]
        public int QuotaRemaining { get; init;}
    }
}
