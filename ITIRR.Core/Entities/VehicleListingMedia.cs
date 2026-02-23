using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class VehicleListingMedia : BaseEntity
    {
        public Guid VehicleListingId { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
        public string MediaType { get; set; } = "Interior"; // Interior/Exterior
        public int DisplayOrder { get; set; } = 0;

        public virtual VehicleListing VehicleListing { get; set; } = null!;
    }
}