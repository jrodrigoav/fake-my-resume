using FakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet()]
        public IActionResult GetUser()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.Identity.Name;
            var userInfo = _userService.GetUserByUserName(email);
            return Ok(userInfo);
        }
    }
}
