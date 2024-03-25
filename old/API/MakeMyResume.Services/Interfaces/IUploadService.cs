using Microsoft.AspNetCore.Http;

namespace MakeMyResume.Services.Interfaces
{
    public interface IUploadService
    {
        byte[] DownloadFile (string fileName);

        Task<string> UploadFile(IFormFile file);
    }
}
