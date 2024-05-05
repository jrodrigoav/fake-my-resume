namespace FakeMyResume.Models;

public class WorkExperience
{
    public DateTime DateBegin { get; set; }

    public DateTime DateEnd { get; set; }

    public string CompanyName { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> Technologies { get; set; } = [];
}
