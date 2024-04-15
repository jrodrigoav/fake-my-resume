using FluentValidation;
using FluentValidation.AspNetCore;
using FakeMyResume.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.API.Controllers;

[ApiController, Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("auth/{message}")]
    public IActionResult TestAuth(string? message)
    {
        var user = HttpContext.User;
        return Ok(message);
    }

    [HttpPost]
    public IActionResult TestValidator(ResumeDTO resumeDTO, [FromServices] IValidator<ResumeDTO> validator)
    {
        var validationResult = validator.Validate(resumeDTO);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }
        return Ok(resumeDTO);

    }
}