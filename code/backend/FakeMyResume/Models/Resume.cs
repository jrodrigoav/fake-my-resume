namespace FakeMyResume.Models;

public class Resume
{
    public string FullName { get; set; } = null!;

    public string CurrentRole { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string[] Certifications { get; set; } = [];

    //public WorkExperience[] WorkExperience { get; set; } = [];//this was changed otherwise we get the "Collection navigations cannot be arrays" error
    public List<WorkExperience> WorkExperience { get; set; } = [];

    //public Education[] Education { get; set; } = [];//this was changed otherwise we get the "Collection navigations cannot be arrays" error
    public List<Education> Education { get; set; } = [];
}
