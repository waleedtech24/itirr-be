using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;

namespace ITIRR.Core.Entities
{
    public class VehicleBrand : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public Guid VehicleTypeId { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual VehicleType VehicleType { get; set; } = null!;
        public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}