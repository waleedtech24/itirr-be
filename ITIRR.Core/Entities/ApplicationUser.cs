using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITIRR.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePhotoUrl { get; set; }

        // Account Info
        public string AccountType { get; set; } = "Individual"; // Individual/Agency
        public string UserType { get; set; } = "Renter"; // Renter/Host/Admin
        public string? Bio { get; set; }

        // Stripe Integration
        public string? StripeCustomerId { get; set; }
        public string? StripeConnectAccountId { get; set; }

        // Verification
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
