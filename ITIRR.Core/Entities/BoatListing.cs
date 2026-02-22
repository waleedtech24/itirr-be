using ITIRR.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ITIRR.Core.Entities
{
    public class BoatListing : BaseEntity
    {
        public Guid CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;

        public string BoatType { get; set; } = string.Empty;
        public string BoatMake { get; set; } = string.Empty;
        public string BoatModel { get; set; } = string.Empty;
        public string BoatYear { get; set; } = string.Empty;
        public string BoatLength { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string HullMaterial { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;

        public string PrimaryGoal { get; set; } = string.Empty;
        public string UsageFrequency { get; set; } = string.Empty;
        public string ShareFrequency { get; set; } = string.Empty;

        public string AdvanceNotice { get; set; } = string.Empty;
        public string MinTripDuration { get; set; } = string.Empty;
        public string MaxTripDuration { get; set; } = string.Empty;
        public bool MinTwoDayWeekend { get; set; }

        public string SkipperFirstName { get; set; } = string.Empty;
        public string? SkipperMiddleName { get; set; }
        public string SkipperLastName { get; set; } = string.Empty;
        public string SkipperLicenceNumber { get; set; } = string.Empty;
        public string SkipperLicenceType { get; set; } = string.Empty;
        public DateTime SkipperLicenceExpiry { get; set; }
        public string? SkipperLicenceDocUrl { get; set; }

        public string OwnerId { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public int CurrentStep { get; set; } = 1;

        public ICollection<BoatListingMedia> Media { get; set; } = new List<BoatListingMedia>();
    }

    public class BoatListingMedia : BaseEntity
    {
        public Guid BoatListingId { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty; 
        public int DisplayOrder { get; set; }
        public BoatListing BoatListing { get; set; } = null!;
    }
}