using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string MessageText { get; set; } = string.Empty;
        public Guid? RelatedVehicleId { get; set; }
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public string? AttachmentUrl { get; set; }

        public virtual ApplicationUser Sender { get; set; } = null!;
        public virtual ApplicationUser Receiver { get; set; } = null!;
        public virtual Vehicle? RelatedVehicle { get; set; }
    }
}