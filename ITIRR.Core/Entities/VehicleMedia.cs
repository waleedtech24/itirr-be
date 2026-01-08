using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class VehicleMedia : BaseEntity
    {
        public Guid VehicleId { get; set; }
        public string MediaType { get; set; } = "Image";
        public string MediaUrl { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; } = false;
        public string? Caption { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}