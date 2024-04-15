using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string id);

    Task<User> CreateUserAsync(User user);
}
