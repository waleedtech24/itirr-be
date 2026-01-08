using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;

namespace ITIRR.Core.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string PhoneCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<State> States { get; set; } = new List<State>();
        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}