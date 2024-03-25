using MakeMyResume.Data.Models;


namespace MakeMyResume.Services.Interfaces
{
    public interface IDocumentGenerationService
    {
        Stream GenerateResumeInPDF(Resume generatePDF);
    }
}
