

namespace ITIRR.Core.DTOs.VehicleListing
{
    public class Step1LocationRequest
    {
        public Guid? ListingId { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
    }

    public class Step2VINRequest
    {
        public Guid ListingId { get; set; }
        public string VIN { get; set; } = string.Empty;
        public bool IsOlderThan1981 { get; set; }
        public string LicencePlateNumber { get; set; } = string.Empty;
    }

    public class Step3OdometerRequest
    {
        public Guid ListingId { get; set; }
        public string OdometerReading { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
    }

    public class Step4HistoryRequest
    {
        public Guid ListingId { get; set; }
        public bool TaxCertified { get; set; }
        public bool HasSalvageTitle { get; set; }
    }

    public class Step5GoalsRequest
    {
        public Guid ListingId { get; set; }
        public string PrimaryGoal { get; set; } = string.Empty;
        public string UsageFrequency { get; set; } = string.Empty;
        public string ShareFrequency { get; set; } = string.Empty;
    }

    public class Step6AvailabilityRequest
    {
        public Guid ListingId { get; set; }
        public string AdvanceNotice { get; set; } = string.Empty;
        public string MinTripDuration { get; set; } = string.Empty;
        public string MaxTripDuration { get; set; } = string.Empty;
        public bool MinTwoDayWeekend { get; set; }
    }

    public class Step8DriverLicenceRequest
    {
        public Guid ListingId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string LicenceNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Step9PCORequest
    {
        public Guid ListingId { get; set; }
        public string VehicleMakeModel { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleColor { get; set; } = string.Empty;
        public string YearOfManufacture { get; set; } = string.Empty;
        public string PHVLicenceNumber { get; set; } = string.Empty;
        public string PHVLicenceExpiryDate { get; set; } = string.Empty;
    }

    public class VehicleListingResponse
    {
        public Guid ListingId { get; set; }
        public string VehicleType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CurrentStep { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}