namespace FakeMyResume.Services.Interfaces;

public interface IAditionalSkillsService
{
    Task<List<string>> GetAditionalSkills(string filter);
}
