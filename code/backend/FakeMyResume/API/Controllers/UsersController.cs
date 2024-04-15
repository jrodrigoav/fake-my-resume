using Microsoft.AspNetCore.Mvc;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FakeMyResume.API.Controllers;

[ApiController, Authorize, Route("api/user")]
public class UsersController(IUserService userService) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        if (userId == null || userName == null)
            return new UnauthorizedResult();

        try
        {
            var user = await userService.GetUserByIdAsync(userId);
            return Ok(user);
        } catch(InvalidOperationException)
        {
            var newUser = new User()
            {
                Id = userId,
                UserName = userName
            };
            var user = await userService.CreateUserAsync(newUser);
            return Ok(user);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            var createdUser = await userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }
        return BadRequest(ModelState);
    }
}
