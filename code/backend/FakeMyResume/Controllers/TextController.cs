using FakeMyResume.Models;
using FakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.Controllers;

[ApiController, Authorize, Route("api/text")]
public class TextController(TextService textService) : ControllerBase
{
    [HttpPost("format")]
    public async Task<ActionResult<string>> ApplyFormatAndSuggestions([FromBody] Message message)
    {
        if (string.IsNullOrWhiteSpace(message.Value)) return BadRequest("A text value should be provided.");
        var result = await textService.GetSuggestions(message.Value);
        if (string.IsNullOrWhiteSpace(result)) return NoContent();
        return Ok(result);
    }
}
