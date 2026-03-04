namespace ITIRR.Core.DTOs.User
{
    public class AppUserDocumentResponse
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}