using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleLocation : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public Guid CityId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsPrimary { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual City City { get; set; } = null!;
    }
}