using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface ITagService
{
    Task<List<Tag>> GetTags(string text);

    Task<int> CreateTags(IEnumerable<Tag> tags);
}
