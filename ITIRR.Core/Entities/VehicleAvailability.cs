using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleAvailability : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Notes { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}