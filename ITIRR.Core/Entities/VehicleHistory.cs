using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleHistory : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string ChangedBy { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ApplicationUser ChangedByUser { get; set; } = null!;
    }
}