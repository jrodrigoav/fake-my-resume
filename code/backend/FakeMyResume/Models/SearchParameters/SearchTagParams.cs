using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.Models.SearchParameters;

public class SearchTagParams
{
    [Required, MinLength(1)]
    public required string Text { get; set; }

    [Range(10, 100, ErrorMessage = "The limit must be a value between 10 and 100.")]
    public int Limit { get; set; } = 50;
}
