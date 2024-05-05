namespace FakeMyResume.Models;

public class Resume
{
    public string FullName { get; set; } = null!;

    public string CurrentRole { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string[] Certifications { get; set; } = [];

    public WorkExperience[] WorkExperience { get; set; } = [];

    public Education[] Education { get; set; } = [];
}
