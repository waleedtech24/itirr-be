using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIRR.Core.DTOs.User
{
    public class AppUserListingDetailResponse
    {
        public Guid ListingId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? State { get; set; }
        public decimal PricePerDay { get; set; }
        public double? Rating { get; set; }
        public string? PartnerName { get; set; }
        public List<string> PhotoUrls { get; set; } = new();
        public CarSpecsDto? CarSpecs { get; set; }
        public JetSpecsDto? JetSpecs { get; set; }
        public BoatSpecsDto? BoatSpecs { get; set; }
    }

    public class CarSpecsDto
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? Transmission { get; set; }
        public string? OdometerReading { get; set; }
    }

    public class JetSpecsDto
    {
        public string? AircraftMake { get; set; }
        public string? AircraftModel { get; set; }
        public string? AircraftYear { get; set; }
        public int? PassengerCapacity { get; set; }
        public string? HomeAirport { get; set; }
        public string? AircraftCategory { get; set; }
    }

    public class BoatSpecsDto
    {
        public string? BoatType { get; set; }
        public string? BoatMake { get; set; }
        public string? BoatModel { get; set; }
        public string? BoatYear { get; set; }
        public int? PassengerCapacity { get; set; }
        public string? BoatLength { get; set; }
    }
}
