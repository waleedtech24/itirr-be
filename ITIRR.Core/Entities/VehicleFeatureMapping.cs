using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleFeatureMapping : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public Guid VehicleFeatureId { get; set; }
        public bool IsAvailable { get; set; } = true;

        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual VehicleFeature VehicleFeature { get; set; } = null!;
    }
}