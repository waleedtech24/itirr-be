using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;

namespace ITIRR.Core.Entities
{
    public class VehicleType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string BookingType { get; set; } = "Direct";
        public bool IsActive { get; set; } = true;
        public int? DisplayOrder { get; set; }

        public virtual ICollection<VehicleCategory> VehicleCategories { get; set; } = new List<VehicleCategory>();
        public virtual ICollection<VehicleBrand> VehicleBrands { get; set; } = new List<VehicleBrand>();
        public virtual ICollection<VehicleFeature> VehicleFeatures { get; set; } = new List<VehicleFeature>();
    }
}