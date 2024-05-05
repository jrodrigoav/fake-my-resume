using FakeMyResume.Models.Data;
using FakeMyResume.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class UserService(FakeMyResumeDbContext context)
{
    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await context.User.FirstAsync(u => u.Id == id);
        user.LastActivity = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        context.User.Add(user);
        await context.SaveChangesAsync();
        return user;
    }
}
