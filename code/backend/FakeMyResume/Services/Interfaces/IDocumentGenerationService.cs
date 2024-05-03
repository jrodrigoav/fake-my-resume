using FakeMyResume.Data.Models;

namespace FakeMyResume.Services.Interfaces;

public interface IDocumentGenerationService
{
    Stream GenerateResumeInPDF(Resume generatePDF);
}
