namespace FakeMyResume.Models.Data;

public class DataResume
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public Resume ResumeData { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdated { get; set; }

    public User User { get; set; } = null!;
}
