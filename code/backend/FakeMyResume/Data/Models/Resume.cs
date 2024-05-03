namespace FakeMyResume.Data.Models;

public class Resume
{
    public string FullName { get; set; } = null!;

    public string CurrentRole { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> Certifications { get; set; } = [];

    public List<WorkExperience> WorkExperience { get; set; } = [];

    public List<Education> Education { get; set; } = [];
}
