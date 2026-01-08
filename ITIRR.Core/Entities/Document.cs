using ITIRR.Core.Entities.Common;
using System;

namespace ITIRR.Core.Entities
{
    public class Document : BaseEntity
    {
        public Guid DocumentTypeId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid? EntityId { get; set; }
        public string? UserId { get; set; }

        public string DocumentUrl { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public bool IsVerified { get; set; } = false;
        public DateTime? VerifiedAt { get; set; }
        public string? VerifiedBy { get; set; }
        public string? RejectionReason { get; set; }

        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsExpired => ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow;
        public bool IsExpiringSoon => ExpiryDate.HasValue &&
                                     ExpiryDate.Value > DateTime.UtcNow &&
                                     ExpiryDate.Value <= DateTime.UtcNow.AddDays(30);

        public virtual DocumentType DocumentType { get; set; } = null!;
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationUser? VerifiedByUser { get; set; }
    }
}