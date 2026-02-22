using ITIRR.Core.Interfaces;

namespace ITIRR.Services.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _webRootPath;

        public FileUploadService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folder)
        {
            var uploadsFolder = Path.Combine(_webRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return $"/uploads/{folder}/{uniqueFileName}";
        }

        public void DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;
            var filePath = Path.Combine(_webRootPath, fileUrl.TrimStart('/'));
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}