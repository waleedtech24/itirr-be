using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string DataType { get; set; } = "String";
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEditable { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }
}