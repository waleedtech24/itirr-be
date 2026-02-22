using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class DriversLicence : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string LicenceNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsVerified { get; set; } = false;

        public virtual ApplicationUser User { get; set; } = null!;
    }
}