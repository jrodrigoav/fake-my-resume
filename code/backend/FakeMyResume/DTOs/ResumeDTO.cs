using FakeMyResume.Models.Data;

namespace FakeMyResume.DTOs;

public class ResumeDTO : CreateResumeDTO
{
    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public static ResumeDTO FromData(DataResume data)
    {
        return new ResumeDTO();
    }
}
