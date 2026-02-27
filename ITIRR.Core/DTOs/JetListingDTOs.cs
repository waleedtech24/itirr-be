namespace ITIRR.Core.DTOs.JetListing
{
    public class JetStep1LocationRequest
    {
        public Guid? ListingId { get; set; }
        public Guid CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string HangarLocation { get; set; } = string.Empty;
        public string HomeAirport { get; set; } = string.Empty;
    }

    public class JetStep2SpecsRequest
    {
        public Guid ListingId { get; set; }
        public string AircraftMake { get; set; } = string.Empty;
        public string AircraftModel { get; set; } = string.Empty;
        public string AircraftYear { get; set; } = string.Empty;
        public string TailNumber { get; set; } = string.Empty;
        public string AircraftCategory { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string RangeNauticalMiles { get; set; } = string.Empty;
        public string CruisingSpeed { get; set; } = string.Empty;
        public string EngineType { get; set; } = string.Empty;
    }

    public class JetStep4CabinRequest
    {
        public Guid ListingId { get; set; }
        public List<string> CabinFeatures { get; set; } = new();
    }

    public class JetStep5SafetyRequest
    {
        public Guid ListingId { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public List<string> SafetyCertifications { get; set; } = new();
    }

    public class JetStep6PilotRequest
    {
        public Guid ListingId { get; set; }
        public string PilotFirstName { get; set; } = string.Empty;
        public string PilotLastName { get; set; } = string.Empty;
        public string PilotLicenceNumber { get; set; } = string.Empty;
        public string PilotLicenceType { get; set; } = string.Empty;
        public DateTime PilotLicenceExpiry { get; set; }
        public int CrewCount { get; set; }
    }

    public class JetStep7AvailabilityRequest
    {
        public Guid ListingId { get; set; }
        public string AdvanceNotice { get; set; } = string.Empty;
        public string MinTripDuration { get; set; } = string.Empty;
        public string MaxTripDuration { get; set; } = string.Empty;
        public string CancellationPolicy { get; set; } = string.Empty;
    }

    public class JetStep8GoalsRequest
    {
        public Guid ListingId { get; set; }
        public string PrimaryGoal { get; set; } = string.Empty;
        public string UsageFrequency { get; set; } = string.Empty;
        public string ShareFrequency { get; set; } = string.Empty;
    }

    public class JetListingResponse
    {
        public Guid ListingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CurrentStep { get; set; }
        public string State { get; set; } = string.Empty;
        public string AircraftMake { get; set; } = string.Empty;
        public string AircraftModel { get; set; } = string.Empty;
        public string AircraftYear { get; set; } = string.Empty;
        public string TailNumber { get; set; } = string.Empty;
        public string AircraftCategory { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string HomeAirport { get; set; } = string.Empty;
        public string HangarLocation { get; set; } = string.Empty;
        public string PrimaryGoal { get; set; } = string.Empty;
        public string? FirstPhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class JetFullDataResponse
    {
        public Guid ListingId { get; set; }
        public string Status { get; set; } = "";
        public int CurrentStep { get; set; }
        public Guid? CountryId { get; set; }
        public string? State { get; set; }
        public Guid? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? HangarLocation { get; set; }
        public string? HomeAirport { get; set; }
        public string? AircraftMake { get; set; }
        public string? AircraftModel { get; set; }
        public string? AircraftYear { get; set; }
        public string? TailNumber { get; set; }
        public string? AircraftCategory { get; set; }
        public int PassengerCapacity { get; set; }
        public string? RangeNauticalMiles { get; set; }
        public string? CruisingSpeed { get; set; }
        public string? EngineType { get; set; }
        public string? CabinFeatures { get; set; }
        public string? SafetyCertifications { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public string? PilotFirstName { get; set; }
        public string? PilotLastName { get; set; }
        public string? PilotLicenceNumber { get; set; }
        public string? PilotLicenceType { get; set; }
        public DateTime? PilotLicenceExpiry { get; set; }
        public int CrewCount { get; set; }
        public string? AdvanceNotice { get; set; }
        public string? MinTripDuration { get; set; }
        public string? MaxTripDuration { get; set; }
        public string? CancellationPolicy { get; set; }
        public string? PrimaryGoal { get; set; }
        public string? UsageFrequency { get; set; }
        public string? ShareFrequency { get; set; }
    }
}