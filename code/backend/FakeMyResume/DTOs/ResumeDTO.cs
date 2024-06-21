using FakeMyResume.Models.Data;

namespace FakeMyResume.DTOs;

public class ResumeDTO : CreateResumeDTO
{
    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public string FullName { get; set; }
    public string Description { get; set; }
    public string CurrentRole { get; set; }

    public static ResumeDTO FromData(DataResume data)
    {
        var itm =new ResumeDTO();
        itm.Id = data.Id;
        itm.FullName = data.User.UserName;
        itm.Description = data.ResumeData.Description;
        itm.CurrentRole = data.ResumeData.CurrentRole;
        return itm;
    }
}
