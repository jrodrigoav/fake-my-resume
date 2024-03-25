using MakeMyResume.Data;
using MakeMyResume.Data.Models;
using MakeMyResume.Services.Interfaces;
using System.Text.Json;

namespace MakeMyResume.Services
{
    public class ResumeService : IResumeService
    {
        private readonly MakeMyResumeDb _context;
        private readonly IDocumentGenerationService _documentGenerationService;

        public ResumeService(MakeMyResumeDb context, IDocumentGenerationService documentGenerationService)
        {
            _context = context;
            _documentGenerationService = documentGenerationService;
        }
        public void SaveResume(Resume resume)
        {
            DataResume dataResume = new DataResume();

            dataResume.AccountId = Guid.NewGuid().ToString(); //resume.AccountId;
            dataResume.JsonData = JsonSerializer.Serialize<Resume>(resume);
            dataResume.CreatedDate = DateTime.UtcNow;
            dataResume.LastUpdated = DateTime.UtcNow;
            _context.DataResume.Add(dataResume);

            _context.SaveChanges();
        }

  
        public Resume? UpdateResume(Resume resume)
        {
            DataResume? dataResume = FindResume(resume.Id);

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
    }
}
