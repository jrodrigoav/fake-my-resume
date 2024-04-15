using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface IResumeService
{
    void SaveResume(Resume resume);
    Resume? GetResume(int id);
    Resume? UpdateResume(Resume resume);
    Stream? GetResumePDF(int id);
    IEnumerable<Resume> GetResumes(string accountId);
}
