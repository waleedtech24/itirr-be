namespace ITIRR.Core.DTOs.BoatListing
{
    public class BoatStep1LocationRequest
    {
        public Guid? ListingId { get; set; }
        public Guid CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;
    }

    public class BoatEditRequest
    {
        public Guid? CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid? CityId { get; set; }
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
    }

    public class BoatStep2DetailsRequest
    {
        public Guid ListingId { get; set; }
        public string BoatType { get; set; } = string.Empty;
        public string BoatMake { get; set; } = string.Empty;
        public string BoatModel { get; set; } = string.Empty;
        public string BoatYear { get; set; } = string.Empty;
        public string BoatLength { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string HullMaterial { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
    }

    public class BoatStep3GoalsRequest
    {
        public Guid ListingId { get; set; }
        public string PrimaryGoal { get; set; } = string.Empty;
        public string UsageFrequency { get; set; } = string.Empty;
        public string ShareFrequency { get; set; } = string.Empty;
    }

    public class BoatStep5AvailabilityRequest
    {
        public Guid ListingId { get; set; }
        public string AdvanceNotice { get; set; } = string.Empty;
        public string MinTripDuration { get; set; } = string.Empty;
        public string MaxTripDuration { get; set; } = string.Empty;
        public bool MinTwoDayWeekend { get; set; }
    }

    public class BoatStep6SkipperRequest
    {
        public Guid ListingId { get; set; }
        public string SkipperFirstName { get; set; } = string.Empty;
        public string? SkipperMiddleName { get; set; }
        public string SkipperLastName { get; set; } = string.Empty;
        public string SkipperLicenceNumber { get; set; } = string.Empty;
        public string SkipperLicenceType { get; set; } = string.Empty;
        public DateTime SkipperLicenceExpiry { get; set; }
    }

    public class BoatListingResponse
    {
        public Guid ListingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CurrentStep { get; set; }
        public string State { get; set; } = string.Empty;
        public string BoatType { get; set; } = string.Empty;
        public string BoatMake { get; set; } = string.Empty;
        public string BoatModel { get; set; } = string.Empty;
        public string BoatYear { get; set; } = string.Empty;
        public string BoatLength { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string PrimaryGoal { get; set; } = string.Empty;
        public string? FirstPhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class BoatFullDataResponse
    {
        public Guid ListingId { get; set; }
        public string Status { get; set; } = "";
        public int CurrentStep { get; set; }
        public Guid? CountryId { get; set; }
        public string? State { get; set; }
        public Guid? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? StreetAddress { get; set; }
        public string? BoatType { get; set; }
        public string? BoatMake { get; set; }
        public string? BoatModel { get; set; }
        public string? BoatYear { get; set; }
        public string? BoatLength { get; set; }
        public int PassengerCapacity { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? HullMaterial { get; set; }
        public string? FuelType { get; set; }
        public string? PrimaryGoal { get; set; }
        public string? UsageFrequency { get; set; }
        public string? ShareFrequency { get; set; }
        public string? AdvanceNotice { get; set; }
        public string? MinTripDuration { get; set; }
        public string? MaxTripDuration { get; set; }
        public bool MinTwoDayWeekend { get; set; }
        public string? SkipperFirstName { get; set; }
        public string? SkipperMiddleName { get; set; }
        public string? SkipperLastName { get; set; }
        public string? SkipperLicenceNumber { get; set; }
        public string? SkipperLicenceType { get; set; }
        public DateTime? SkipperLicenceExpiry { get; set; }
    }
}