using Microsoft.AspNetCore.Mvc;
using FakeMyResume.Services.Interfaces;

namespace FakeMyResume.API.Controllers;

[ApiController, Route("api/aditional-skills")]
public class AditionalSkillsController : ControllerBase
{
    private readonly IAditionalSkillsService _aditionalSkillsService;

    public AditionalSkillsController(IAditionalSkillsService aditionalSkillsService)
    {
        _aditionalSkillsService = aditionalSkillsService;
    }

    [HttpGet("{filter}")]
    public async Task<IEnumerable<string>> GetAditionalSkills(string filter)
    {
        var aditionalSkills = await _aditionalSkillsService.GetAditionalSkills(filter);
        return aditionalSkills;
    }
}
