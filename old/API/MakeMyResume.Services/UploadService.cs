using MakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;

namespace MakeMyResume.Services
{
    public class UploadService : IUploadService
    {
        IConfiguration _configuration;
        private string connectionString;
        private string containerName;
        public UploadService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["AzureStorageSettings:ConnectionString"];
            containerName = _configuration["AzureStorageSettings:ContainerName"];
        }

        public byte[] DownloadFile(string fileUrl)
        {
            try
            {
                WebClient webClient = new WebClient();
                if (string.IsNullOrWhiteSpace(fileUrl))
                {
                    throw new ArgumentException("La URL del archivo no es válida.");
                }
                string fileName = Path.GetFileName(fileUrl);
                string sasUrl = GenerateSasUri(connectionString, containerName, fileName, TimeSpan.FromDays(1));
                byte[] fileBytes = webClient.DownloadData(sasUrl);

                return fileBytes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al descargar el archivo: {ex.Message}");
            }
        }



        public string GenerateSasUri(string connectionString, string containerName, string blobName, TimeSpan expiryTime)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            SharedAccessBlobPolicy sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read, 
                SharedAccessExpiryTime = DateTime.UtcNow.Add(expiryTime) 
            };

            string sasToken = blob.GetSharedAccessSignature(sasPolicy);
            Uri blobSasUri = new Uri(blob.Uri + sasToken);

            return blobSasUri.ToString();
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string connectionString = _configuration["AzureStorageSettings:ConnectionString"];

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("makemyresume");

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

                    using (Stream stream = file.OpenReadStream())
                    {
                        await blob.UploadFromStreamAsync(stream);
                    }

                    return blob.Uri.ToString();
                }

                throw new Exception("No se proporcionó ningún archivo para cargar.");
            }
            catch (Exception ex)
            {
                throw new Exception( $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
