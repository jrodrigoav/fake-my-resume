namespace FakeMyResume.Data.Models;

public class WorkExperience
{
    public DateTime DateBegin { get; set; }

    public DateTime DateEnd { get; set; }

    public string? CompanyName { get; set; }

    public string? ProjectName { get; set; }

    public string? Role { get; set; }

    public string? Description { get; set; }
}
