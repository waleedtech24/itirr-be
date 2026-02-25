using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class PCOLicence : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public Guid? VehicleListingId { get; set; }
        public string VehicleMakeModel { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleColor { get; set; } = string.Empty;
        public string YearOfManufacture { get; set; } = string.Empty;
        public string PHVLicenceNumber { get; set; } = string.Empty;
        public string PHVLicenceExpiryDate { get; set; } = string.Empty;
        public string? RoadTaxCertificateUrl { get; set; }
        public string? MOTCertificateUrl { get; set; }
        public string? RoadTaxCertificate2Url { get; set; }
        public string? VehicleLogbookUrl { get; set; }
        public bool IsVerified { get; set; } = false;

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual VehicleListing? VehicleListing { get; set; }
    }
}