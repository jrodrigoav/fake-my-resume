using FakeMyResume.Data.Models;
using FakeMyResume.DTOs;
using FakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.API.Controllers;

[ApiController, Authorize, Route("api/tag")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpGet("{text}")]
    public async Task<IActionResult> SearchTags(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return NotFound();
        }
        var tags = await tagService.GetTags(text);
        if (tags == null || tags.Count == 0)
        {
            return NotFound();
        }
        return Ok(tags.Select(t => t.Name));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTags(IEnumerable<CreateTagDTO> tags)
    {
        try
        {
            var newTags = tags.Select(t => new Tag { Name = t.Name });
            var result = await tagService.CreateTags(newTags);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        }
    }
}
