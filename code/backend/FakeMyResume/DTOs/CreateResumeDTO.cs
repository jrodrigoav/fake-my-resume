namespace FakeMyResume.DTOs;

public class CreateResumeDTO
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? CurrentRole { get; set; }

    public string? Description { get; set; }

    public List<string>? Technologies { get; set; }

    public List<string> Certifications { get; set; } = [];

    public List<WorkExperienceDTO>? WorkExperience { get; set; }

    public List<EducationDTO>? Education { get; set; }
}
