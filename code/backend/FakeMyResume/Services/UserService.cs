using FakeMyResume.Models.Data;
using FakeMyResume.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        if(string.IsNullOrEmpty( user.Id)) user.Id = Guid.NewGuid().ToString();
        context.User.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    //public async Task<User> GetUserByUserNameAsync(string userName)
    //{
    //    var user = await context.User.FirstAsync(u => u.UserName == userName);
    //    user.LastActivity = DateTime.UtcNow;
    //    await context.SaveChangesAsync();
    //    return user;
    //}
    //private readonly FakeMyResumeDbContext _context;

    

    public User GetUserByUserName(string userName)
    {
        return context.User.FirstOrDefault(x => x.UserName == userName);
    }
}
