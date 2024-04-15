namespace FakeMyResume.Data.Models;

public class DataResume
{
    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public string JsonData { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdated { get; set; }

}
