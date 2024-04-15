using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface ITagService
{
    List<Tag> GetTags(string text);
}
