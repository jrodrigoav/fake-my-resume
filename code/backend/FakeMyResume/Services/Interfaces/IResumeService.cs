using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface IResumeService
{
    Resume SaveResume(Resume resume, string accountId);

    Resume? GetResume(int id);

    Resume? UpdateResume(Resume resume);

    Stream? GetResumePDF(int id);

    IEnumerable<Resume> GetResumes(string accountId);
}
