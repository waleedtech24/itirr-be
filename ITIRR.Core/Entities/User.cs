using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class User : BaseEntity
    {
        // ── Auth ──
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public string Role { get; set; } = "User";

        // ── Basic Info ──
        public string? AgencyName { get; set; }
        public string? About { get; set; }
        public string? Languages { get; set; }
        public string? ProfilePhotoUrl { get; set; }

        // ── Contact ──
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }

        // ── Navigation ──
        public ICollection<AppUserDocument> Documents { get; set; } = new List<AppUserDocument>();
    }
}