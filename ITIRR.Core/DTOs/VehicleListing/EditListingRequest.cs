using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.DTOs.VehicleListing
{
    public class EditListingRequest
    {
        public Guid ListingId { get; set; }

        public Guid? CountryId { get; set; }
        public string? State { get; set; }
        public Guid? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? StreetAddress { get; set; }

        public string? VIN { get; set; }
        public bool IsOlderThan1981 { get; set; }
        public string? LicencePlateNumber { get; set; }

        public string? OdometerReading { get; set; }
        public string? Transmission { get; set; }
        public List<string> Features { get; set; } = new();

        public bool TaxCertified { get; set; }
        public bool HasSalvageTitle { get; set; }

        public string? PrimaryGoal { get; set; }
        public string? UsageFrequency { get; set; }
        public string? ShareFrequency { get; set; }

        public string? AdvanceNotice { get; set; }
        public string? MinTripDuration { get; set; }
        public string? MaxTripDuration { get; set; }
        public bool MinTwoDayWeekend { get; set; }

        public string? VehicleMakeModel { get; set; }
        public string? PlateNumber { get; set; }
        public string? VehicleColor { get; set; }
        public string? YearOfManufacture { get; set; }
        public string? PHVLicenceNumber { get; set; }
        public string? PHVLicenceExpiryDate { get; set; }
    }
}
