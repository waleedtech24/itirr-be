using ITIRR.Core.DTOs.User;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITIRR.Services
{
    public class AppUserListingService : IAppUserListingService
    {
        private readonly ApplicationDbContext _context;

        public AppUserListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<AppUserListingCardResponse> Items, int TotalCount)>
            SearchListingsAsync(AppUserListingSearchRequest request)
        {
            var results = new List<AppUserListingCardResponse>();

            // ── Cars ──
            if (request.Type == null || request.Type.ToLower() == "car")
            {
                var cars = _context.VehicleListings
                    .Where(v => v.Status == "Active")
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Location))
                    cars = cars.Where(v => v.State != null &&
                        v.State.ToLower().Contains(request.Location.ToLower()));

                var carItems = await cars
                    .Include(v => v.Media)
                    .Include(v => v.Owner)
                    .Select(v => new AppUserListingCardResponse
                    {
                        ListingId = v.Id,
                        Type = "car",
                        Title = v.VehicleMakeModel ?? "",
                        Location = v.State ?? v.StreetAddress ?? "",
                        PricePerDay = 0,
                        FirstPhotoUrl = v.Media != null && v.Media.Any()
                            ? v.Media.OrderBy(m => m.DisplayOrder).First().MediaUrl
                            : null,
                        PassengerCapacity = null,
                        PartnerName = v.Owner != null
                            ? v.Owner.FirstName + " " + v.Owner.LastName
                            : null
                    }).ToListAsync();

                results.AddRange(carItems);
            }

            // ── Jets ──
            if (request.Type == null || request.Type.ToLower() == "jet")
            {
                var jets = _context.JetListings
                    .Where(j => j.Status == "Active")
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Location))
                    jets = jets.Where(j => j.State != null &&
                        j.State.ToLower().Contains(request.Location.ToLower()));

                var jetItems = await jets
                    .Include(j => j.Media)
                    .Include(j => j.Owner)
                    .Select(j => new AppUserListingCardResponse
                    {
                        ListingId = j.Id,
                        Type = "jet",
                        Title = j.AircraftMake + " " + j.AircraftModel,
                        Location = j.State ?? j.HomeAirport ?? "",
                        PricePerDay = 0,
                        FirstPhotoUrl = j.Media != null && j.Media.Any()
                            ? j.Media.OrderBy(m => m.DisplayOrder).First().MediaUrl
                            : null,
                        PassengerCapacity = j.PassengerCapacity,
                        PartnerName = j.Owner != null
                            ? j.Owner.FirstName + " " + j.Owner.LastName
                            : null
                    }).ToListAsync();

                results.AddRange(jetItems);
            }

            // ── Boats ──
            if (request.Type == null || request.Type.ToLower() == "boat")
            {
                var boats = _context.BoatListings
                    .Where(b => b.Status == "Active")
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Location))
                    boats = boats.Where(b => b.State != null &&
                        b.State.ToLower().Contains(request.Location.ToLower()));

                var boatItems = await boats
                    .Include(b => b.Media)
                    .Include(b => b.Owner)
                    .Select(b => new AppUserListingCardResponse
                    {
                        ListingId = b.Id,
                        Type = "boat",
                        Title = b.BoatMake + " " + b.BoatModel,
                        Location = b.State ?? "",
                        PricePerDay = 0,
                        FirstPhotoUrl = b.Media != null && b.Media.Any()
                            ? b.Media.OrderBy(m => m.DisplayOrder).First().MediaUrl
                            : null,
                        PassengerCapacity = b.PassengerCapacity,
                        PartnerName = b.Owner != null
                            ? b.Owner.FirstName + " " + b.Owner.LastName
                            : null
                    }).ToListAsync();

                results.AddRange(boatItems);
            }

            // ── Price filter (post-merge) ──
            if (request.MinPrice.HasValue)
                results = results.Where(r => r.PricePerDay >= request.MinPrice.Value).ToList();

            if (request.MaxPrice.HasValue)
                results = results.Where(r => r.PricePerDay <= request.MaxPrice.Value).ToList();

            var totalCount = results.Count;

            var paged = results
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return (paged, totalCount);
        }

        public async Task<AppUserListingDetailResponse?> GetListingDetailAsync(Guid listingId)
        {
            // ── Try Car ──
            var car = await _context.VehicleListings
                .Include(v => v.Media)
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == listingId && v.Status == "Active");

            if (car != null)
                return new AppUserListingDetailResponse
                {
                    ListingId = car.Id,
                    Type = "car",
                    Title = car.VehicleMakeModel ?? "",
                    Location = car.StreetAddress ?? "",
                    State = car.State,
                    PricePerDay = 0,
                    PartnerName = car.Owner != null
                        ? car.Owner.FirstName + " " + car.Owner.LastName
                        : null,
                    PhotoUrls = car.Media?
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => m.MediaUrl).ToList() ?? new(),
                    CarSpecs = new CarSpecsDto
                    {
                        Make = car.VehicleMakeModel,
                        Model = null,
                        Year = car.YearOfManufacture,
                        Transmission = car.Transmission,
                        OdometerReading = car.OdometerReading
                    }
                };

            // ── Try Jet ──
            var jet = await _context.JetListings
                .Include(j => j.Media)
                .Include(j => j.Owner)
                .FirstOrDefaultAsync(j => j.Id == listingId && j.Status == "Active");

            if (jet != null)
                return new AppUserListingDetailResponse
                {
                    ListingId = jet.Id,
                    Type = "jet",
                    Title = jet.AircraftMake + " " + jet.AircraftModel,
                    Location = jet.HomeAirport,
                    State = jet.State,
                    PricePerDay = 0,
                    PartnerName = jet.Owner != null
                        ? jet.Owner.FirstName + " " + jet.Owner.LastName
                        : null,
                    PhotoUrls = jet.Media?
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => m.MediaUrl).ToList() ?? new(),
                    JetSpecs = new JetSpecsDto
                    {
                        AircraftMake = jet.AircraftMake,
                        AircraftModel = jet.AircraftModel,
                        AircraftYear = jet.AircraftYear,
                        PassengerCapacity = jet.PassengerCapacity,
                        HomeAirport = jet.HomeAirport,
                        AircraftCategory = jet.AircraftCategory
                    }
                };

            // ── Try Boat ──
            var boat = await _context.BoatListings
                .Include(b => b.Media)
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(b => b.Id == listingId && b.Status == "Active");

            if (boat != null)
                return new AppUserListingDetailResponse
                {
                    ListingId = boat.Id,
                    Type = "boat",
                    Title = boat.BoatMake + " " + boat.BoatModel,
                    Location = boat.State,
                    State = boat.State,
                    PricePerDay = 0,
                    PartnerName = boat.Owner != null
                        ? boat.Owner.FirstName + " " + boat.Owner.LastName
                        : null,
                    PhotoUrls = boat.Media?
                        .OrderBy(m => m.DisplayOrder)
                        .Select(m => m.MediaUrl).ToList() ?? new(),
                    BoatSpecs = new BoatSpecsDto
                    {
                        BoatType = boat.BoatType,
                        BoatMake = boat.BoatMake,
                        BoatModel = boat.BoatModel,
                        BoatYear = boat.BoatYear,
                        PassengerCapacity = boat.PassengerCapacity,
                        BoatLength = boat.BoatLength
                    }
                };

            return null;
        }
    }
}