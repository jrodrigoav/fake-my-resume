using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using System.Text.Json;

namespace FakeMyResume.Services;

public class ResumeService : IResumeService
{
    private readonly MakeMyResumeDb _context;
    private readonly IDocumentGenerationService _documentGenerationService;

    public ResumeService(MakeMyResumeDb context, IDocumentGenerationService documentGenerationService)
    {
        _context = context;
        _documentGenerationService = documentGenerationService;
    }

    public Resume SaveResume(Resume resume, string accountId)
    {
        var dataResume = new DataResume
        {
            AccountId = accountId,
            JsonData = JsonSerializer.Serialize(resume),
            CreatedDate = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        };
        _context.DataResume.Add(dataResume);
        _context.SaveChanges();
        return GetResume(dataResume.Id);
    }

    public Resume? UpdateResume(Resume resume)
    {
        var dataResume = FindResume(resume.Id);

        if (dataResume == null)
        {
            return null;
        }

        dataResume.JsonData = JsonSerializer.Serialize<Resume>(resume);
        dataResume.LastUpdated = DateTime.UtcNow;
        _context.DataResume.Update(dataResume);

        _context.SaveChanges();

        return resume;
    }

    public Stream? GetResumePDF(int id)
    {
        var resume = GetResume(id);
        return resume != null ? _documentGenerationService.GenerateResumeInPDF(resume) : null;
    }

    private DataResume? FindResume(int id)
    {
        return _context.DataResume.FirstOrDefault(x => x.Id == id);
    }

    public Resume? GetResume(int id)
    {
        DataResume? dataResume = FindResume(id);

        Resume? resume = null;

        if (dataResume != null)
        {
            resume = JsonSerializer.Deserialize<Resume>(dataResume.JsonData);
        }
        return resume;
    }

    public IEnumerable<Resume> GetResumes(string accountId)
    {
        return _context.DataResume.Where(resume => resume.AccountId.Equals(accountId))
            .AsEnumerable()
            .Select(resume => JsonSerializer.Deserialize<Resume>(resume.JsonData))
            .Where(resume => resume != null)!;
    }
}
