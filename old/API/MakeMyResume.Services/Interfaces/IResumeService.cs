using MakeMyResume.Data.Models;

namespace MakeMyResume.Services.Interfaces
{
    public interface IResumeService
    {
        void SaveResume(Resume resume);
        Resume? GetResume(int id);
        Resume? UpdateResume(Resume resume);
        Stream? GetResumePDF(int id);
    }
}
