using FakeMyResume.Data.Models;
using FakeMyResume.DTOs;
using FakeMyResume.Models.SearchParameters;
using FakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.API.Controllers;

[ApiController, Authorize, Route("api/tag")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SearchTags([FromQuery] SearchTagParams search)
    {
        var tags = await tagService.GetTags(search);
        return Ok(tags.Select(t => t.Name));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTags(IEnumerable<CreateTagDTO> tags)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
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
