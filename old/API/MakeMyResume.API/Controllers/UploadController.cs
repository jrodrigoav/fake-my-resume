using MakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MakeMyResume.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
               string urlFile = await _uploadService.UploadFile(file);
                return Ok(urlFile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult DownloadFile(string fileUrl)
        {
            try
            {
                byte[] fileBytes = _uploadService.DownloadFile(fileUrl);
                string fileName = Path.GetFileName(fileUrl);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }





    }
}
