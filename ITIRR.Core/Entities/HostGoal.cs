using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class HostGoal : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public string HostId { get; set; } = string.Empty;
        public string GoalType { get; set; } = "Monthly";
        public decimal TargetAmount { get; set; }
        public string Currency { get; set; } = "GBP";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
        public bool IsAchieved { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ApplicationUser Host { get; set; } = null!;
    }
}