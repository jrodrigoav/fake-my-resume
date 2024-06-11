using FakeMyResume.Models.Data;
using FakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
                return Ok(new User() {  UserName = User.Identity.Name});
            return new UnauthorizedResult();
        }
    }
}
