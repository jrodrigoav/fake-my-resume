namespace FakeMyResume.Models.Data;

public class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    //public DataResume[] Resumes { get; set; } = [];//this was changed otherwise we get the "Collection navigations cannot be arrays" error
    public List<DataResume> Resumes { get; set; } = [];
}
