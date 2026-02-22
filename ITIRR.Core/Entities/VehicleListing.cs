using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class VehicleListing : BaseEntity
    {
        public string OwnerId { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty; // Car/Jet/Boat
        public string Status { get; set; } = "Draft"; // Draft/Pending/Active
        public int CurrentStep { get; set; } = 1;

        // Step 1 - Location
        public Guid? CountryId { get; set; }
        public string? State { get; set; }
        public Guid? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? StreetAddress { get; set; }

        // Step 2 - VIN
        public string? VIN { get; set; }
        public bool IsOlderThan1981 { get; set; } = false;
        public string? LicencePlateNumber { get; set; }

        // Step 3 - Odometer
        public string? OdometerReading { get; set; }
        public string? Transmission { get; set; } // Automatic/Manual
        public string? Features { get; set; } // JSON array

        // Step 4 - Car History
        public bool? TaxCertified { get; set; }
        public bool? HasSalvageTitle { get; set; }

        // Step 5 - Goals
        public string? PrimaryGoal { get; set; }
        public string? UsageFrequency { get; set; }
        public string? ShareFrequency { get; set; }

        // Step 6 - Availability
        public string? AdvanceNotice { get; set; }
        public string? MinTripDuration { get; set; }
        public string? MaxTripDuration { get; set; }
        public bool MinTwoDayWeekend { get; set; } = false;

        // Navigation
        public virtual ApplicationUser Owner { get; set; } = null!;
        public virtual Country? Country { get; set; }
        public virtual City? City { get; set; }
        public virtual ICollection<VehicleListingMedia> Media { get; set; } = new List<VehicleListingMedia>();
    }
}