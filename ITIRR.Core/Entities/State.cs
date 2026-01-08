using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIRR.Core.Entities.Common;

namespace ITIRR.Core.Entities
{
    public class State : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}
