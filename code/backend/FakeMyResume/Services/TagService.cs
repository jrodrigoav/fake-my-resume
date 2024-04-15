using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class TagService(MakeMyResumeDb context) : ITagService
{
    public Task<List<Tag>> GetTags(string text)
    {
        return context.Tag.Where(t => t.Name.Replace(" ", string.Empty).ToLower().Contains(text)).ToListAsync();
    }

    public async Task<int> CreateTags(IEnumerable<Tag> tags)
    {
        await context.Tag.AddRangeAsync(tags);
        return await context.SaveChangesAsync();
    }
}
