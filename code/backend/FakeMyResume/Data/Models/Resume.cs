namespace FakeMyResume.Data.Models;

public class Resume
{
    public int Id { get; set; }
    public string AccountId { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public string? CurrentRole { get; set; }

    public string? Email { get; set; }

    public string? Description { get; set; }

    public List<string>? Technologies { get; set; }

    public List<string>? Methodologies { get; set; }

    public List<string>? Certifications { get; set; }

    public List<WorkExperience>? WorkExperience { get; set; }

    public List<Education>? Education { get; set; }
}
