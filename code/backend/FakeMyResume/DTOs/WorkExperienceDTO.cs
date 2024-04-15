namespace FakeMyResume.DTOs;

public class WorkExperienceDTO
{
    public int FromYear { get; set; }

    public int FromMonth { get; set; }

    public int ToYear { get; set; }

    public int ToMonth { get; set; }

    public string? CompanyName { get; set; }

    public string? ProjectName { get; set; }

    public string? Role { get; set; }

    public string? Description { get; set; }

    public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
}
