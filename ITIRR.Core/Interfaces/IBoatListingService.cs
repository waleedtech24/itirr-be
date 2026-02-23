using ITIRR.Core.DTOs.BoatListing;
using ITIRR.Core.Entities;

namespace ITIRR.Core.Interfaces
{
    public interface IBoatListingService
    {
        Task<BoatListingResponse> SaveStep1Async(BoatStep1LocationRequest request, string userId);
        Task<BoatListingResponse> SaveStep2Async(BoatStep2DetailsRequest request);
        Task<BoatListingResponse> SaveStep3Async(BoatStep3GoalsRequest request);
        Task<BoatListingResponse> SaveStep4PhotosAsync(Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos);
        Task<BoatListingResponse> SaveStep5Async(BoatStep5AvailabilityRequest request);
        Task<BoatListingResponse> SaveStep6Async(BoatStep6SkipperRequest request,
            (Stream Stream, string FileName)? licenceDoc);
        Task<BoatListing?> GetInProgressListingAsync(string userId);
        Task<BoatFullDataResponse?> GetListingFullDataAsync(Guid listingId);
        Task<IEnumerable<BoatListing>> GetMyListingsAsync(string userId);
    }
}