using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.DTOs.VehicleListing
{
    public class ListingFullDataResponse
    {
        public Guid ListingId { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CurrentStep { get; set; }

        // Step 1
        public Guid? CountryId { get; set; }
        public string? State { get; set; }
        public Guid? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? StreetAddress { get; set; }

        // Step 2
        public string? VIN { get; set; }
        public bool IsOlderThan1981 { get; set; }
        public string? LicencePlateNumber { get; set; }

        // Step 3
        public string? OdometerReading { get; set; }
        public string? Transmission { get; set; }
        public string? Features { get; set; }

        // Step 4
        public bool? TaxCertified { get; set; }
        public bool? HasSalvageTitle { get; set; }

        // Step 5
        public string? PrimaryGoal { get; set; }
        public string? UsageFrequency { get; set; }
        public string? ShareFrequency { get; set; }

        // Step 6
        public string? AdvanceNotice { get; set; }
        public string? MinTripDuration { get; set; }
        public string? MaxTripDuration { get; set; }
        public bool MinTwoDayWeekend { get; set; }
    }
}
