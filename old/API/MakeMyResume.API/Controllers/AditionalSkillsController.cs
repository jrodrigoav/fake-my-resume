using Microsoft.AspNetCore.Mvc;
using MakeMyResume.Services.Interfaces;
using MakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;

namespace MakeMyResume.API.Controllers
{
    [ApiController, Route("api/aditional-skills")]
    public class AditionalSkillsController : ControllerBase
    {
        private readonly IAditionalSkillsService _aditionalSkillsService;

        public AditionalSkillsController(IAditionalSkillsService aditionalSkillsService)
        {
            _aditionalSkillsService = aditionalSkillsService;
        }

        [HttpGet("{filter}"), AllowAnonymous]
        public async Task<IEnumerable<string>> GetAditionalSkills(string filter)
        {
            var aditionalSkills = await _aditionalSkillsService.GetAditionalSkills(filter);
            return aditionalSkills;
        }
    }
}
