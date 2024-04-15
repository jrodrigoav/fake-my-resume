using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class TagService(TagsDbContext context) : ITagService
{
    public Task<List<Tag>> GetTags(string text)
    {
        return context.Tags.Where(t => t.Name.Contains(text.ToLower())).Take(20).ToListAsync();
    }

    public async Task<int> CreateTags(IEnumerable<Tag> tags)
    {
        await context.Tags.AddRangeAsync(tags);
        return await context.SaveChangesAsync();
    }
}
