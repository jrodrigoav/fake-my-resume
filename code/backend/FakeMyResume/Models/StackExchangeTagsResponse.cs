using FakeMyResume.Data.Models;
using System.Text.Json.Serialization;

namespace FakeMyResume.Models;

public class StackExchangeTagsResponse
{
    [JsonPropertyName("items")]
    public Tag[] Items { get; init; } = [];

    [JsonPropertyName("has_more")]
    public bool HasMore { get; init; }

    [JsonPropertyName("quota_max")]
    public int QuotaMax { get; init; }

    [JsonPropertyName("quota_remaining")]
    public int QuotaRemaining { get; init; }
}
