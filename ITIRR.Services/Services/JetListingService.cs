using ITIRR.Core.DTOs.JetListing;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ITIRR.Services.Services
{
    public class JetListingService : IJetListingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public JetListingService(ApplicationDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<JetListingResponse> SaveStep1Async(JetStep1LocationRequest request, string userId)
        {
            JetListing listing;
            if (request.ListingId.HasValue && request.ListingId != Guid.Empty)
            {
                listing = await _context.JetListings.AsNoTracking()
                    .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                    ?? throw new Exception("Listing not found");

                await _context.JetListings.Where(v => v.Id == request.ListingId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(v => v.CountryId, request.CountryId)
                        .SetProperty(v => v.State, request.State)
                        .SetProperty(v => v.CityId, request.CityId)
                        .SetProperty(v => v.ZipCode, request.ZipCode)
                        .SetProperty(v => v.HangarLocation, request.HangarLocation)
                        .SetProperty(v => v.HomeAirport, request.HomeAirport)
                        .SetProperty(v => v.CurrentStep, 1)
                        .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));
            }
            else
            {
                listing = new JetListing
                {
                    OwnerId = userId,
                    CountryId = request.CountryId,
                    State = request.State,
                    CityId = request.CityId,
                    ZipCode = request.ZipCode,
                    HangarLocation = request.HangarLocation,
                    HomeAirport = request.HomeAirport,
                    CurrentStep = 1,
                    Status = "Draft"
                };
                await _context.JetListings.AddAsync(listing);
                await _context.SaveChangesAsync();
            }
            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 1, Status = "Draft", Message = "Step 1 saved" };
        }

        public async Task<JetListingResponse> SaveStep2Async(JetStep2SpecsRequest request)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.AircraftMake, request.AircraftMake)
                    .SetProperty(v => v.AircraftModel, request.AircraftModel)
                    .SetProperty(v => v.AircraftYear, request.AircraftYear)
                    .SetProperty(v => v.TailNumber, request.TailNumber)
                    .SetProperty(v => v.AircraftCategory, request.AircraftCategory)
                    .SetProperty(v => v.PassengerCapacity, request.PassengerCapacity)
                    .SetProperty(v => v.RangeNauticalMiles, request.RangeNauticalMiles)
                    .SetProperty(v => v.CruisingSpeed, request.CruisingSpeed)
                    .SetProperty(v => v.EngineType, request.EngineType)
                    .SetProperty(v => v.CurrentStep, 2)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 2, Status = listing.Status, Message = "Step 2 saved" };
        }

        public async Task<JetListingResponse> SaveStep3PhotosAsync(Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == listingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            var existing = await _context.JetListingMedia
                .Where(m => m.JetListingId == listingId).ToListAsync();
            if (existing.Any()) _context.JetListingMedia.RemoveRange(existing);

            int order = 1;
            foreach (var (stream, fileName) in interiorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "jet-listings");
                _context.JetListingMedia.Add(new JetListingMedia
                { JetListingId = listingId, MediaUrl = url, MediaType = "Interior", DisplayOrder = order++ });
            }
            foreach (var (stream, fileName) in exteriorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "jet-listings");
                _context.JetListingMedia.Add(new JetListingMedia
                { JetListingId = listingId, MediaUrl = url, MediaType = "Exterior", DisplayOrder = order++ });
            }

            await _context.JetListings.Where(v => v.Id == listingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.CurrentStep, 3)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            await _context.SaveChangesAsync();
            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 3, Status = listing.Status, Message = "Photos uploaded" };
        }

        public async Task<JetListingResponse> SaveStep4Async(JetStep4CabinRequest request)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            var featuresJson = JsonSerializer.Serialize(request.CabinFeatures);
            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.CabinFeatures, featuresJson)
                    .SetProperty(v => v.CurrentStep, 4)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 4, Status = listing.Status, Message = "Step 4 saved" };
        }

        public async Task<JetListingResponse> SaveStep5Async(JetStep5SafetyRequest request,
            (Stream Stream, string FileName)? airworthiness,
            (Stream Stream, string FileName)? insurance,
            (Stream Stream, string FileName)? registration)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            string? airUrl = airworthiness != null ? await _fileUploadService.UploadFileAsync(airworthiness.Value.Stream, airworthiness.Value.FileName, "jet-docs") : null;
            string? insUrl = insurance != null ? await _fileUploadService.UploadFileAsync(insurance.Value.Stream, insurance.Value.FileName, "jet-docs") : null;
            string? regUrl = registration != null ? await _fileUploadService.UploadFileAsync(registration.Value.Stream, registration.Value.FileName, "jet-docs") : null;
            var certsJson = JsonSerializer.Serialize(request.SafetyCertifications);

            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.LastMaintenanceDate, request.LastMaintenanceDate)
                    .SetProperty(v => v.SafetyCertifications, certsJson)
                    .SetProperty(v => v.AirworthinessDocUrl, airUrl ?? listing.AirworthinessDocUrl)
                    .SetProperty(v => v.InsuranceDocUrl, insUrl ?? listing.InsuranceDocUrl)
                    .SetProperty(v => v.RegistrationDocUrl, regUrl ?? listing.RegistrationDocUrl)
                    .SetProperty(v => v.CurrentStep, 5)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 5, Status = listing.Status, Message = "Step 5 saved" };
        }

        public async Task<JetListingResponse> SaveStep6Async(JetStep6PilotRequest request)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.PilotFirstName, request.PilotFirstName)
                    .SetProperty(v => v.PilotLastName, request.PilotLastName)
                    .SetProperty(v => v.PilotLicenceNumber, request.PilotLicenceNumber)
                    .SetProperty(v => v.PilotLicenceType, request.PilotLicenceType)
                    .SetProperty(v => v.PilotLicenceExpiry, request.PilotLicenceExpiry)
                    .SetProperty(v => v.CrewCount, request.CrewCount)
                    .SetProperty(v => v.CurrentStep, 6)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 6, Status = listing.Status, Message = "Step 6 saved" };
        }

        public async Task<JetListingResponse> SaveStep7Async(JetStep7AvailabilityRequest request)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.AdvanceNotice, request.AdvanceNotice)
                    .SetProperty(v => v.MinTripDuration, request.MinTripDuration)
                    .SetProperty(v => v.MaxTripDuration, request.MaxTripDuration)
                    .SetProperty(v => v.CancellationPolicy, request.CancellationPolicy)
                    .SetProperty(v => v.CurrentStep, 7)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 7, Status = listing.Status, Message = "Step 7 saved" };
        }

        public async Task<JetListingResponse> SaveStep8Async(JetStep8GoalsRequest request)
        {
            var listing = await _context.JetListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.JetListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.PrimaryGoal, request.PrimaryGoal)
                    .SetProperty(v => v.UsageFrequency, request.UsageFrequency)
                    .SetProperty(v => v.ShareFrequency, request.ShareFrequency)
                    .SetProperty(v => v.CurrentStep, 8)
                    .SetProperty(v => v.Status, "Pending")
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new JetListingResponse { ListingId = listing.Id, CurrentStep = 8, Status = "Pending", Message = "Jet listing submitted!" };
        }

        public async Task<JetListing?> GetInProgressListingAsync(string userId) =>
            await _context.JetListings
                .Where(v => v.OwnerId == userId && v.Status == "Draft" && !v.IsDeleted)
                .OrderByDescending(v => v.UpdatedAt).AsNoTracking().FirstOrDefaultAsync();

        public async Task<JetFullDataResponse?> GetListingFullDataAsync(Guid listingId)
        {
            var l = await _context.JetListings.AsNoTracking().FirstOrDefaultAsync(v => v.Id == listingId);
            if (l == null) return null;
            return new JetFullDataResponse
            {
                ListingId = l.Id,
                Status = l.Status,
                CurrentStep = l.CurrentStep,
                CountryId = l.CountryId,
                State = l.State,
                CityId = l.CityId,
                ZipCode = l.ZipCode,
                HangarLocation = l.HangarLocation,
                HomeAirport = l.HomeAirport,
                AircraftMake = l.AircraftMake,
                AircraftModel = l.AircraftModel,
                AircraftYear = l.AircraftYear,
                TailNumber = l.TailNumber,
                AircraftCategory = l.AircraftCategory,
                PassengerCapacity = l.PassengerCapacity,
                RangeNauticalMiles = l.RangeNauticalMiles,
                CruisingSpeed = l.CruisingSpeed,
                EngineType = l.EngineType,
                CabinFeatures = l.CabinFeatures,
                SafetyCertifications = l.SafetyCertifications,
                LastMaintenanceDate = l.LastMaintenanceDate,
                PilotFirstName = l.PilotFirstName,
                PilotLastName = l.PilotLastName,
                PilotLicenceNumber = l.PilotLicenceNumber,
                PilotLicenceType = l.PilotLicenceType,
                PilotLicenceExpiry = l.PilotLicenceExpiry,
                CrewCount = l.CrewCount,
                AdvanceNotice = l.AdvanceNotice,
                MinTripDuration = l.MinTripDuration,
                MaxTripDuration = l.MaxTripDuration,
                CancellationPolicy = l.CancellationPolicy,
                PrimaryGoal = l.PrimaryGoal,
                UsageFrequency = l.UsageFrequency,
                ShareFrequency = l.ShareFrequency
            };
        }

        public async Task<IEnumerable<JetListing>> GetMyListingsAsync(string userId) =>
            await _context.JetListings
                .Include(j => j.Media)
                .Where(v => v.OwnerId == userId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
    }
}