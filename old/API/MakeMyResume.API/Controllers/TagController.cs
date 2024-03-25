using MakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MakeMyResume.API.Controllers
{
    [ApiController, Route("api/tag"), Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService) 
        {
            _tagService = tagService;
        }

        [HttpGet("{text}"), AllowAnonymous]
        public IActionResult GetResumen(string text)
        {
            if (text.IsNullOrEmpty())
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
}
