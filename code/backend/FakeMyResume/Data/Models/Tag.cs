using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FakeMyResume.Data.Models;

public class Tag
{
    [JsonPropertyName("name"), StringLength(50)]
    public string Name { get; init; } = null!;

    [JsonPropertyName("has_synonyms")]
    public bool HasSynonyms { get; init; }

    [JsonPropertyName("is_moderator_only")]
    public bool IsModeratorOnly { get; init; }

    [JsonPropertyName("is_required")]
    public bool IsRequired { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }
}
