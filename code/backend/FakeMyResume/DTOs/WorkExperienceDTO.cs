namespace FakeMyResume.DTOs;

public class WorkExperienceDTO
{
    public DateTime DateBegin { get; set; }

    public DateTime DateEnd { get; set; }

    public string? CompanyName { get; set; }

    public string? ProjectName { get; set; }

    public string? Role { get; set; }

    public string? Description { get; set; }

    public List<string>? Technologies { get; set; }
}
