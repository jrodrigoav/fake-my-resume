using FakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.API.Controllers;

[ApiController, Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService) 
    {
        _tagService = tagService;
    }

    [HttpGet("{text}")]
    public IActionResult GetResumen(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return NotFound();
        }
        var tags = _tagService.GetTags(text);
        if (tags == null || tags.Count == 0)
        {
            return NotFound();
        }
        return Ok(tags);

    }
}
