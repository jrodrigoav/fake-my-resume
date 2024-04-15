using FakeMyResume.Data.Models;
using System.Text.Json;

namespace FakeMyResume.DTOs;

public class ResumeDTO : CreateResumeDTO
{
    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public static ResumeDTO FromData(DataResume data)
    {
        var resume = JsonSerializer.Deserialize<ResumeDTO>(data.JsonData) ?? throw new NullReferenceException("Failed to deserialize resume data.");
        resume.Id = data.Id;
        resume.AccountId = data.AccountId;
        return resume;
    }
}
