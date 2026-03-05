using ITIRR.Core.DTOs.VehicleListing;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ITIRR.Services.Services
{
    public class VehicleListingService :IVehicleListingService
    {
        private readonly IVehicleListingRepository _listingRepository;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogService _logService;
        private readonly ApplicationDbContext _context;

        public VehicleListingService(
            IVehicleListingRepository listingRepository,
            ApplicationDbContext context,
            IFileUploadService fileUploadService,
            ILogService logService)
        {
            _listingRepository = listingRepository;
            _fileUploadService = fileUploadService;
            _logService = logService;
            _context= context;

        }

        public async Task<VehicleListingResponse> SaveStep1Async(
            Step1LocationRequest request, string userId)
        {
            VehicleListing listing;

            if (request.ListingId.HasValue && request.ListingId != Guid.Empty)
            {
                listing = await _listingRepository.GetByIdAsync(request.ListingId.Value)
                    ?? throw new Exception("Listing not found");

                listing.VehicleType = request.VehicleType;
                listing.CountryId = request.CountryId;
                listing.State = request.State;
                listing.CityId = request.CityId;
                listing.ZipCode = request.ZipCode;
                listing.StreetAddress = request.StreetAddress;
                listing.CurrentStep = 1;
                listing.UpdatedAt = DateTime.UtcNow;
                await _listingRepository.UpdateAsync(listing);
            }
            else
            {
                listing = new VehicleListing
                {
                    OwnerId = userId,
                    VehicleType = request.VehicleType,
                    CountryId = request.CountryId,
                    State = request.State,
                    CityId = request.CityId,
                    ZipCode = request.ZipCode,
                    StreetAddress = request.StreetAddress,
                    CurrentStep = 1,
                    Status = "Draft"
                };
                await _listingRepository.AddAsync(listing);
            }

            await _logService.LogInformationAsync(userId, "Step1Saved",
                "VehicleListing", listing.Id, "Step 1 location saved");

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                VehicleType = listing.VehicleType,
                Status = listing.Status,
                CurrentStep = 1,
                Message = "Step 1 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep2Async(Step2VINRequest request)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.VIN = request.VIN;
            listing.IsOlderThan1981 = request.IsOlderThan1981;
            listing.LicencePlateNumber = request.LicencePlateNumber;
            listing.CurrentStep = 2;
            listing.UpdatedAt = DateTime.UtcNow;

            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 2,
                Message = "Step 2 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep3Async(Step3OdometerRequest request)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.OdometerReading = request.OdometerReading;
            listing.Transmission = request.Transmission;
            listing.Features = JsonSerializer.Serialize(request.Features);
            listing.CurrentStep = 3;
            listing.UpdatedAt = DateTime.UtcNow;

            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 3,
                Message = "Step 3 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep4Async(Step4HistoryRequest request)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.TaxCertified = request.TaxCertified;
            listing.HasSalvageTitle = request.HasSalvageTitle;
            listing.CurrentStep = 4;
            listing.UpdatedAt = DateTime.UtcNow;

            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 4,
                Message = "Step 4 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep5Async(Step5GoalsRequest request)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.PrimaryGoal = request.PrimaryGoal;
            listing.UsageFrequency = request.UsageFrequency;
            listing.ShareFrequency = request.ShareFrequency;
            listing.CurrentStep = 5;
            listing.UpdatedAt = DateTime.UtcNow;

            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 5,
                Message = "Step 5 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep6Async(Step6AvailabilityRequest request)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.AdvanceNotice = request.AdvanceNotice;
            listing.MinTripDuration = request.MinTripDuration;
            listing.MaxTripDuration = request.MaxTripDuration;
            listing.MinTwoDayWeekend = request.MinTwoDayWeekend;
            listing.CurrentStep = 6;
            listing.UpdatedAt = DateTime.UtcNow;

            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 6,
                Message = "Step 6 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep7PhotosAsync(
            Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos)
        {
            var listing = await _context.VehicleListings
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == listingId && !v.IsDeleted)
                ?? throw new Exception("Listing not found");

            var existingMedia = await _context.VehicleListingMedia
                .Where(m => m.VehicleListingId == listingId)
                .ToListAsync();
            if (existingMedia.Any())
                _context.VehicleListingMedia.RemoveRange(existingMedia);

            int displayOrder = 1;
            foreach (var (stream, fileName) in interiorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "vehicle-listings");
                _context.VehicleListingMedia.Add(new VehicleListingMedia
                {
                    VehicleListingId = listingId,
                    MediaUrl = url,
                    MediaType = "Interior",
                    DisplayOrder = displayOrder++
                });
            }

            foreach (var (stream, fileName) in exteriorPhotos)
            {
                var url = await _fileUploadService.UploadFileAsync(stream, fileName, "vehicle-listings");
                _context.VehicleListingMedia.Add(new VehicleListingMedia
                {
                    VehicleListingId = listingId,
                    MediaUrl = url,
                    MediaType = "Exterior",
                    DisplayOrder = displayOrder++
                });
            }

            await _context.VehicleListings
                .Where(v => v.Id == listingId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(v => v.CurrentStep, 7)
                    .SetProperty(v => v.UpdatedAt, DateTime.UtcNow));

            await _context.SaveChangesAsync();

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                VehicleType = listing.VehicleType,
                Status = listing.Status,
                CurrentStep = 7,
                Message = "Photos uploaded successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep8Async(
            Step8DriverLicenceRequest request, string userId)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            listing.CurrentStep = 8;
            listing.UpdatedAt = DateTime.UtcNow;
            await _listingRepository.UpdateAsync(listing);

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 8,
                Message = "Step 8 saved successfully"
            };
        }

        public async Task<VehicleListingResponse> SaveStep9Async(
            Step9PCORequest request,
            (Stream Stream, string FileName)? roadTax,
            (Stream Stream, string FileName)? mot,
            (Stream Stream, string FileName)? roadTax2,
            (Stream Stream, string FileName)? logbook,
            string userId)
        {
            var listing = await _listingRepository.GetByIdAsync(request.ListingId)
                ?? throw new Exception("Listing not found");

            if (roadTax.HasValue)
                await _fileUploadService.UploadFileAsync(
                    roadTax.Value.Stream, roadTax.Value.FileName, "pco-docs");

            if (mot.HasValue)
                await _fileUploadService.UploadFileAsync(
                    mot.Value.Stream, mot.Value.FileName, "pco-docs");

            if (roadTax2.HasValue)
                await _fileUploadService.UploadFileAsync(
                    roadTax2.Value.Stream, roadTax2.Value.FileName, "pco-docs");

            if (logbook.HasValue)
                await _fileUploadService.UploadFileAsync(
                    logbook.Value.Stream, logbook.Value.FileName, "pco-docs");

            listing.CurrentStep = 9;
            listing.Status = "Pending";
            listing.UpdatedAt = DateTime.UtcNow;
            await _listingRepository.UpdateAsync(listing);

            await _logService.LogInformationAsync(userId, "ListingSubmitted",
                "VehicleListing", listing.Id, "Listing submitted for review");

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                CurrentStep = 9,
                Status = "Pending",
                Message = "Listing submitted for review successfully"
            };
        }

        public async Task<VehicleListing?> GetListingByIdAsync(Guid id)
            => await _listingRepository.GetByIdAsync(id);

        public async Task<IEnumerable<VehicleListing>> GetMyListingsAsync(string userId)
        {
            return await _context.VehicleListings
                .Include(v => v.Media)   
                .Where(v => v.OwnerId == userId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<VehicleListing?> GetInProgressListingAsync(string userId)
        {
            var listings = await _listingRepository.GetByOwnerAsync(userId);
            return listings
                .Where(l => l.Status == "Draft")
                .OrderByDescending(l => l.UpdatedAt)
                .FirstOrDefault();
        }

        public async Task<ListingFullDataResponse?> GetListingFullDataAsync(Guid listingId)
        {
            var listing = await _listingRepository.GetByIdAsync(listingId);
            if (listing == null) return null;

            return new ListingFullDataResponse
            {
                ListingId = listing.Id,
                VehicleType = listing.VehicleType ?? "car",
                Status = listing.Status,
                CurrentStep = listing.CurrentStep,

                // Step 1
                CountryId = listing.CountryId,
                State = listing.State,
                CityId = listing.CityId,
                ZipCode = listing.ZipCode,
                StreetAddress = listing.StreetAddress,

                // Step 2
                VIN = listing.VIN,
                IsOlderThan1981 = listing.IsOlderThan1981,
                LicencePlateNumber = listing.LicencePlateNumber,

                // Step 3
                OdometerReading = listing.OdometerReading,
                Transmission = listing.Transmission,
                Features = listing.Features,

                // Step 4
                TaxCertified = listing.TaxCertified,
                HasSalvageTitle = listing.HasSalvageTitle,

                // Step 5
                PrimaryGoal = listing.PrimaryGoal,
                UsageFrequency = listing.UsageFrequency,
                ShareFrequency = listing.ShareFrequency,

                // Step 6
                AdvanceNotice = listing.AdvanceNotice,
                MinTripDuration = listing.MinTripDuration,
                MaxTripDuration = listing.MaxTripDuration,
                MinTwoDayWeekend = listing.MinTwoDayWeekend,

                // Step 9 
                VehicleMakeModel = listing.VehicleMakeModel,
                PlateNumber = listing.PlateNumber,
                VehicleColor = listing.VehicleColor,
                YearOfManufacture = listing.YearOfManufacture,
                PHVLicenceNumber = listing.PHVLicenceNumber,
                PHVLicenceExpiryDate = listing.PHVLicenceExpiryDate,

                FirstPhotoUrl = listing.Media != null && listing.Media.Any(m => m.MediaType == "Exterior")
                                ? listing.Media.First(m => m.MediaType == "Exterior").MediaUrl
                                : listing.Media != null && listing.Media.Any()
                                ? listing.Media.First().MediaUrl
                                : null,
                CreatedAt = listing.CreatedAt
            };
        }

        public async Task<VehicleListingResponse> SaveEditAsync(
    Guid id, EditListingRequest request, string userId, bool submit)
        {
            var listing = await _context.VehicleListings
                .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == userId)
                ?? throw new Exception("Listing not found");

            listing.CountryId = request.CountryId;
            listing.State = request.State;
            listing.CityId = request.CityId;
            listing.ZipCode = request.ZipCode;
            listing.StreetAddress = request.StreetAddress;
            listing.VIN = request.VIN;
            listing.IsOlderThan1981 = request.IsOlderThan1981;
            listing.LicencePlateNumber = request.LicencePlateNumber;
            listing.OdometerReading = request.OdometerReading;
            listing.Transmission = request.Transmission;
            listing.Features = System.Text.Json.JsonSerializer.Serialize(request.Features);
            listing.TaxCertified = request.TaxCertified;
            listing.HasSalvageTitle = request.HasSalvageTitle;
            listing.PrimaryGoal = request.PrimaryGoal;
            listing.UsageFrequency = request.UsageFrequency;
            listing.ShareFrequency = request.ShareFrequency;
            listing.AdvanceNotice = request.AdvanceNotice;
            listing.MinTripDuration = request.MinTripDuration;
            listing.MaxTripDuration = request.MaxTripDuration;
            listing.MinTwoDayWeekend = request.MinTwoDayWeekend;
            listing.VehicleMakeModel = request.VehicleMakeModel;
            listing.PlateNumber = request.PlateNumber;
            listing.VehicleColor = request.VehicleColor;
            listing.YearOfManufacture = request.YearOfManufacture;
            listing.PHVLicenceNumber = request.PHVLicenceNumber;
            listing.PHVLicenceExpiryDate = request.PHVLicenceExpiryDate;
            listing.UpdatedAt = DateTime.UtcNow;

            if (submit)
                listing.Status = "Pending";

            await _context.SaveChangesAsync();

            return new VehicleListingResponse
            {
                ListingId = listing.Id,
                VehicleType = listing.VehicleType ?? "car",
                Status = listing.Status,
                CurrentStep = listing.CurrentStep,
                Message = submit ? "Submitted for review" : "Saved as draft"
            };
        }
    }
}