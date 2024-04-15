using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface IResumeService
{
    DataResume SaveResume(Resume resume, string accountId);

    DataResume GetResume(int id);

    DataResume UpdateResume(int id, Resume resume);

    IEnumerable<DataResume> GetResumes(string accountId);
}
