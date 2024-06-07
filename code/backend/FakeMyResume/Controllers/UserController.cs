using FakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace FakeMyResume.Controllers
{
    [ApiController, Authorize, Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController( UserService userService) {
            _userService = userService;
        }
        [HttpGet("current")]
        public async Task<IActionResult> GetUserAsync()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.Identity.Name;
            var userInfo = _userService.GetUserByUserName(email);
            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name) && userInfo == null)
            {//the user was not in the system but is a valid user
                FakeMyResume.Models.Data.User user = new FakeMyResume.Models.Data.User();
                user.UserName = User.Identity.Name;
                userInfo = await _userService.CreateUserAsync(user);
            }
            return Ok(userInfo);
        }
    }
}
