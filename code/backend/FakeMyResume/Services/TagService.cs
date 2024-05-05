using FakeMyResume.Models.Data;
using FakeMyResume.Models.SearchParameters;
using FakeMyResume.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class TagService(FakeMyResumeDbContext context)
{
    public Task<List<Tag>> GetTags(SearchTagParams search)
    {
        return context.Tags.Where(t => t.Name.Contains(search.Text.ToLower())).Take(search.Limit).ToListAsync();
    }

    public async Task<int> CreateTags(IEnumerable<Tag> tags)
    {
        await context.Tags.AddRangeAsync(tags);
        return await context.SaveChangesAsync();
    }
}
