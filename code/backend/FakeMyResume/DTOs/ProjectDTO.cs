namespace FakeMyResume.DTOs;

public class ProjectDTO
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<string> TechnologiesUsed { get; set; } = new List<string>();
}
