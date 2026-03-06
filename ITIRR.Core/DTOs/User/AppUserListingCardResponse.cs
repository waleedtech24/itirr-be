using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.DTOs.User
{
    public class AppUserListingCardResponse
    {
        public Guid ListingId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public string? FirstPhotoUrl { get; set; }
        public double? Rating { get; set; }
        public int? PassengerCapacity { get; set; }
        public string? PartnerName { get; set; }
    }
}
