using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class JetListing : BaseEntity
    {
        public Guid CountryId { get; set; }
        public string State { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string HangarLocation { get; set; } = string.Empty;
        public string HomeAirport { get; set; } = string.Empty;

        public string AircraftMake { get; set; } = string.Empty;
        public string AircraftModel { get; set; } = string.Empty;
        public string AircraftYear { get; set; } = string.Empty;
        public string TailNumber { get; set; } = string.Empty;
        public string AircraftCategory { get; set; } = string.Empty;
        public int PassengerCapacity { get; set; }
        public string RangeNauticalMiles { get; set; } = string.Empty;
        public string CruisingSpeed { get; set; } = string.Empty;
        public string EngineType { get; set; } = string.Empty;

        public string CabinFeatures { get; set; } = string.Empty; 

        public string AirworthinessDocUrl { get; set; } = string.Empty;
        public string InsuranceDocUrl { get; set; } = string.Empty;
        public string RegistrationDocUrl { get; set; } = string.Empty;
        public DateTime LastMaintenanceDate { get; set; }
        public string SafetyCertifications { get; set; } = string.Empty; // JSON

        public string PilotFirstName { get; set; } = string.Empty;
        public string PilotLastName { get; set; } = string.Empty;
        public string PilotLicenceNumber { get; set; } = string.Empty;
        public string PilotLicenceType { get; set; } = string.Empty;
        public DateTime PilotLicenceExpiry { get; set; }
        public int CrewCount { get; set; }

        public string AdvanceNotice { get; set; } = string.Empty;
        public string MinTripDuration { get; set; } = string.Empty;
        public string MaxTripDuration { get; set; } = string.Empty;
        public string CancellationPolicy { get; set; } = string.Empty;

        public string PrimaryGoal { get; set; } = string.Empty;
        public string UsageFrequency { get; set; } = string.Empty;
        public string ShareFrequency { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public int CurrentStep { get; set; } = 1;

        public ICollection<JetListingMedia> Media { get; set; } = new List<JetListingMedia>();
    }

    public class JetListingMedia : BaseEntity
    {
        public Guid JetListingId { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public JetListing JetListing { get; set; } = null!;
    }
}