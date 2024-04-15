using FakeMyResume.Data.Models;
using FakeMyResume.Models.SearchParameters;

namespace FakeMyResume.Services.Interfaces;

public interface ITagService
{
    Task<List<Tag>> GetTags(SearchTagParams search);

    Task<int> CreateTags(IEnumerable<Tag> tags);
}
