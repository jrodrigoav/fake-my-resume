using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyResume.Services.Interfaces
{
    public interface IAditionalSkillsService
    {
        Task<List<string>> GetAditionalSkills(string filter);
    }
}
