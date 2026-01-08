using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? ActionUrl { get; set; }
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public bool IsSent { get; set; } = false;
        public DateTime? SentAt { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}