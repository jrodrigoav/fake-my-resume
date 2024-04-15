namespace FakeMyResume.Data.Models;

public class User
{
    public required string Id { get; set; }

    public required string UserName { get; set; }

    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    public virtual ICollection<DataResume> Resumes { get; set; } = [];
}
