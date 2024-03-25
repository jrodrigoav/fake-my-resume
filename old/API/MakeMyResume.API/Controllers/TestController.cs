using FluentValidation;
using FluentValidation.AspNetCore;
using MakeMyResume.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MakeMyResume.API.Controllers
{
    [ApiController, Route("api/test"), Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("auth/{message}")]
        public IActionResult TestAuth(string? message)
        {
            var user = HttpContext.User;
            return Ok(message);
        }

        [HttpPost, AllowAnonymous]
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

}