using ITIRR.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ITIRR.Core.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = true;
        public bool RequiresExpiry { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}