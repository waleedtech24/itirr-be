using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;

namespace ITIRR.Core.Entities
{
    public class VehicleCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public Guid VehicleTypeId { get; set; }
        public bool IsActive { get; set; } = true;
        public int? DisplayOrder { get; set; }

        public virtual VehicleType VehicleType { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}