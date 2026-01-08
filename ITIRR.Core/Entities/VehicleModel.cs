using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;

namespace ITIRR.Core.Entities
{
    public class VehicleModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid VehicleBrandId { get; set; }
        public int? Year { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual VehicleBrand VehicleBrand { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}