using ITIRR.Core.DTOs.BoatListing;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ITIRR.Services.Services
{
    public class BoatListingService : IBoatListingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public BoatListingService(ApplicationDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<BoatListingResponse> SaveStep1Async(
            BoatStep1LocationRequest request, string userId)
        {
            BoatListing listing;
            if (request.ListingId.HasValue && request.ListingId != Guid.Empty)
            {
                listing = await _context.BoatListings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                    ?? throw new Exception("Listing not found");

                await _context.BoatListings.Where(v => v.Id == request.ListingId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(v => v.CountryId, request.CountryId)
                        .SetProperty(v => v.State, request.State)
                        .SetProperty(v => v.CityId, request.CityId)
                        .SetProperty(v => v.ZipCode, request.ZipCode)
                        .SetProperty(v => v.StreetAddress, request.StreetAddress)
                        .SetProperty(v => v.CurrentStep, 1)
                        .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));
            }
            else
            {
                listing = new BoatListing
                {
                    OwnerId = userId,
                    CountryId = request.CountryId,
                    State = request.State,
                    CityId = request.CityId,
                    ZipCode = request.ZipCode,
                    StreetAddress = request.StreetAddress,
                    CurrentStep = 1,
                    Status = "Draft"
                };
                await _context.BoatListings.AddAsync(listing);
                await _context.SaveChangesAsync();
            }

            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 1, Status = "Draft", Message = "Step 1 saved" };
        }

        public async Task<BoatListingResponse> SaveStep2Async(BoatStep2DetailsRequest request)
        {
            var listing = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.BoatListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.BoatType, request.BoatType)
                    .SetProperty(v => v.BoatMake, request.BoatMake)
                    .SetProperty(v => v.BoatModel, request.BoatModel)
                    .SetProperty(v => v.BoatYear, request.BoatYear)
                    .SetProperty(v => v.BoatLength, request.BoatLength)
                    .SetProperty(v => v.PassengerCapacity, request.PassengerCapacity)
                    .SetProperty(v => v.RegistrationNumber, request.RegistrationNumber)
                    .SetProperty(v => v.HullMaterial, request.HullMaterial)
                    .SetProperty(v => v.FuelType, request.FuelType)
                    .SetProperty(v => v.CurrentStep, 2)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 2, Status = listing.Status, Message = "Step 2 saved" };
        }

        public async Task<BoatListingResponse> SaveStep3Async(BoatStep3GoalsRequest request)
        {
            var listing = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.BoatListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.PrimaryGoal, request.PrimaryGoal)
                    .SetProperty(v => v.UsageFrequency, request.UsageFrequency)
                    .SetProperty(v => v.ShareFrequency, request.ShareFrequency)
                    .SetProperty(v => v.CurrentStep, 3)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 3, Status = listing.Status, Message = "Step 3 saved" };
        }

        public async Task<BoatListingResponse> SaveStep4PhotosAsync(Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos)
        {
            var listing = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == listingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            var existing = await _context.BoatListingMedia
                .Where(m => m.BoatListingId == listingId).ToListAsync();
            if (existing.Any()) _context.BoatListingMedia.RemoveRange(existing);

            int order = 1;
            foreach (var (stream, fileName) in interiorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "boat-listings");
                _context.BoatListingMedia.Add(new BoatListingMedia
                { BoatListingId = listingId, MediaUrl = url, MediaType = "Interior", DisplayOrder = order++ });
            }
            foreach (var (stream, fileName) in exteriorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "boat-listings");
                _context.BoatListingMedia.Add(new BoatListingMedia
                { BoatListingId = listingId, MediaUrl = url, MediaType = "Exterior", DisplayOrder = order++ });
            }

            await _context.BoatListings.Where(v => v.Id == listingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.CurrentStep, 4)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            await _context.SaveChangesAsync();
            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 4, Status = listing.Status, Message = "Photos uploaded" };
        }

        public async Task<BoatListingResponse> SaveStep5Async(BoatStep5AvailabilityRequest request)
        {
            var listing = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            await _context.BoatListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.AdvanceNotice, request.AdvanceNotice)
                    .SetProperty(v => v.MinTripDuration, request.MinTripDuration)
                    .SetProperty(v => v.MaxTripDuration, request.MaxTripDuration)
                    .SetProperty(v => v.MinTwoDayWeekend, request.MinTwoDayWeekend)
                    .SetProperty(v => v.CurrentStep, 5)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 5, Status = listing.Status, Message = "Step 5 saved" };
        }

        public async Task<BoatListingResponse> SaveStep6Async(BoatStep6SkipperRequest request,
            (Stream Stream, string FileName)? licenceDoc)
        {
            var listing = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == request.ListingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            string? docUrl = null;
            if (licenceDoc != null)
                docUrl = await _fileUploadService.UploadFileAsync(
                    licenceDoc.Value.Stream, licenceDoc.Value.FileName, "skipper-docs");

            await _context.BoatListings.Where(v => v.Id == request.ListingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.SkipperFirstName, request.SkipperFirstName)
                    .SetProperty(v => v.SkipperMiddleName, request.SkipperMiddleName)
                    .SetProperty(v => v.SkipperLastName, request.SkipperLastName)
                    .SetProperty(v => v.SkipperLicenceNumber, request.SkipperLicenceNumber)
                    .SetProperty(v => v.SkipperLicenceType, request.SkipperLicenceType)
                    .SetProperty(v => v.SkipperLicenceExpiry, request.SkipperLicenceExpiry)
                    .SetProperty(v => v.SkipperLicenceDocUrl, docUrl ?? listing.SkipperLicenceDocUrl)
                    .SetProperty(v => v.CurrentStep, 6)
                    .SetProperty(v => v.Status, "Pending")
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            return new BoatListingResponse { ListingId = listing.Id, CurrentStep = 6, Status = "Pending", Message = "Boat listing submitted!" };
        }

        public async Task<BoatListing?> GetInProgressListingAsync(string userId)
        {
            return await _context.BoatListings
                .Where(v => v.OwnerId == userId && v.Status == "Draft" && !v.IsDeleted)
                .OrderByDescending(v => v.UpdatedAt)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<BoatFullDataResponse?> GetListingFullDataAsync(Guid listingId)
        {
            var l = await _context.BoatListings.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == listingId);
            if (l == null) return null;

            return new BoatFullDataResponse
            {
                ListingId = l.Id,
                Status = l.Status,
                CurrentStep = l.CurrentStep,
                CountryId = l.CountryId,
                State = l.State,
                CityId = l.CityId,
                ZipCode = l.ZipCode,
                StreetAddress = l.StreetAddress,
                BoatType = l.BoatType,
                BoatMake = l.BoatMake,
                BoatModel = l.BoatModel,
                BoatYear = l.BoatYear,
                BoatLength = l.BoatLength,
                PassengerCapacity = l.PassengerCapacity,
                RegistrationNumber = l.RegistrationNumber,
                HullMaterial = l.HullMaterial,
                FuelType = l.FuelType,
                PrimaryGoal = l.PrimaryGoal,
                UsageFrequency = l.UsageFrequency,
                ShareFrequency = l.ShareFrequency,
                AdvanceNotice = l.AdvanceNotice,
                MinTripDuration = l.MinTripDuration,
                MaxTripDuration = l.MaxTripDuration,
                MinTwoDayWeekend = l.MinTwoDayWeekend,
                SkipperFirstName = l.SkipperFirstName,
                SkipperMiddleName = l.SkipperMiddleName,
                SkipperLastName = l.SkipperLastName,
                SkipperLicenceNumber = l.SkipperLicenceNumber,
                SkipperLicenceType = l.SkipperLicenceType,
                SkipperLicenceExpiry = l.SkipperLicenceExpiry
            };
        }

        public async Task<IEnumerable<BoatListing>> GetMyListingsAsync(string userId) =>
            await _context.BoatListings
                .Include(b => b.Media)
                .Where(v => v.OwnerId == userId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
    }
}