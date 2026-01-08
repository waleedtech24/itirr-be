using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ITIRR.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid VehicleTypeId { get; set; }
        public Guid VehicleCategoryId { get; set; }
        public Guid VehicleBrandId { get; set; }
        public Guid VehicleModelId { get; set; }
        public int Year { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? VIN { get; set; }

        public string OwnerId { get; set; } = string.Empty;
        public bool IsAgencyOwned { get; set; } = false;

        public decimal PricePerDay { get; set; }
        public decimal? PricePerWeek { get; set; }
        public decimal? PricePerMonth { get; set; }
        public string Currency { get; set; } = "GBP";
        public decimal? SecurityDeposit { get; set; }

        public int PassengerCapacity { get; set; }
        public int? LuggageCapacity { get; set; }

        public string? TypeSpecificData { get; set; }

        public bool IsAvailable { get; set; } = true;
        public bool IsVerified { get; set; } = false;
        public DateTime? VerifiedAt { get; set; }
        public string? VerifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public bool HasInsurance { get; set; } = false;
        public DateTime? InsuranceExpiryDate { get; set; }

        public int ViewCount { get; set; } = 0;
        public int BookingCount { get; set; } = 0;
        public decimal? AverageRating { get; set; }

        public virtual VehicleType VehicleType { get; set; } = null!;
        public virtual VehicleCategory VehicleCategory { get; set; } = null!;
        public virtual VehicleBrand VehicleBrand { get; set; } = null!;
        public virtual VehicleModel VehicleModel { get; set; } = null!;
        public virtual ApplicationUser Owner { get; set; } = null!;

        public virtual ICollection<VehicleLocation> VehicleLocations { get; set; } = new List<VehicleLocation>();
        public virtual ICollection<VehicleMedia> VehicleMedia { get; set; } = new List<VehicleMedia>();
        public virtual ICollection<VehicleAvailability> VehicleAvailabilities { get; set; } = new List<VehicleAvailability>();
        public virtual ICollection<VehicleBlockedDate> VehicleBlockedDates { get; set; } = new List<VehicleBlockedDate>();
        public virtual ICollection<VehicleFeatureMapping> VehicleFeatureMappings { get; set; } = new List<VehicleFeatureMapping>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<HostGoal> HostGoals { get; set; } = new List<HostGoal>();
        public virtual ICollection<VehicleHistory> VehicleHistories { get; set; } = new List<VehicleHistory>();
    }
}