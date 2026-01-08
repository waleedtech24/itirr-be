using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleBlockedDate : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? BlockedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ApplicationUser? BlockedByUser { get; set; }
    }
}