namespace FakeMyResume.Models.Data;

public class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    public List<DataResume> Resumes { get; set; } = [];
}
