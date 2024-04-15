using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using System.Text.Json;

namespace FakeMyResume.Services;

public class ResumeService : IResumeService
{
    private readonly MakeMyResumeDb _context;

    public ResumeService(MakeMyResumeDb context)
    {
        _context = context;
    }

    public DataResume GetResume(int id)
    {
        return _context.DataResume.First(x => x.Id == id);
    }

    public IEnumerable<DataResume> GetResumes(string accountId)
    {
        return _context.DataResume.Where(resume => resume.AccountId.Equals(accountId));
    }

    public DataResume SaveResume(Resume resume, string accountId)
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

    public DataResume UpdateResume(int id, Resume resume)
    {
        var dataResume = GetResume(id);
        dataResume.JsonData = JsonSerializer.Serialize(resume);
        dataResume.LastUpdated = DateTime.UtcNow;
        _context.DataResume.Update(dataResume);
        _context.SaveChanges();
        return dataResume;
    }
}
