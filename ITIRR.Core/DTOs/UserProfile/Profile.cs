namespace ITIRR.Core.DTOs.Profile
{
    // ── GET ──
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public string? AgencyName { get; set; }
        public string? About { get; set; }
        public string? Languages { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
    }

    // ── BASIC UPDATE ──
    public class UpdateBasicInfoRequest
    {
        public string? AgencyName { get; set; }
        public string? About { get; set; }
        public string? Languages { get; set; }
    }

    // ── CONTACT UPDATE ──
    public class UpdateContactRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
    }

    // ── DOCUMENT ──
    public class UserDocumentResponse
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}