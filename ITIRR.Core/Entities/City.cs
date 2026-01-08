using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public Guid? StateId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Country Country { get; set; } = null!;
        public virtual State? State { get; set; }
        public virtual ICollection<VehicleLocation> VehicleLocations { get; set; } = new List<VehicleLocation>();
    }
}
