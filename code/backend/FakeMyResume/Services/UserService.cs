using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeMyResume.Services;

public class UserService(MakeMyResumeDb context) : IUserService
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
