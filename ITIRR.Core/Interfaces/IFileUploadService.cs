using Microsoft.AspNetCore.Http;

namespace ITIRR.Core.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string folder);
        void DeleteFile(string fileUrl);
    }
}