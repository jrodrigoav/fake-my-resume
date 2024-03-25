using MakeMyResume.Data.Models;

namespace MakeMyResume.Services.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetTags(string text);
    }
}
