using FakeMyResume.Models.Data;
using FakeMyResume.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class UserService(FakeMyResumeDbContext context) 
{
    public async Task<User> GetUserByUserNameAsync(string? userName)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.UserName == userName);
        if (string.IsNullOrWhiteSpace(userName) || user == null)
        {
            user = new User() { Id = Guid.NewGuid().ToString(), UserName = userName, LastActivity = DateTime.UtcNow };
            context.Add(user);
        }
        user.LastActivity = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await context.User.FirstAsync(u => u.Id == id);
        user.LastActivity = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        if(string.IsNullOrEmpty( user.Id)) user.Id = Guid.NewGuid().ToString();
        context.User.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public User GetUserByUserName(string userName)
    {
        return context.User.FirstOrDefault(x => x.UserName == userName);
    }
}
