using ITIRR.Core.DTOs.JetListing;
using ITIRR.Core.Entities;

namespace ITIRR.Core.Interfaces
{
    public interface IJetListingService
    {
        Task<JetListingResponse> SaveStep1Async(JetStep1LocationRequest request, string userId);
        Task<JetListingResponse> SaveStep2Async(JetStep2SpecsRequest request);
        Task<JetListingResponse> SaveStep3PhotosAsync(Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos);
        Task<JetListingResponse> SaveStep4Async(JetStep4CabinRequest request);
        Task<JetListingResponse> SaveStep5Async(JetStep5SafetyRequest request,
            (Stream Stream, string FileName)? airworthiness,
            (Stream Stream, string FileName)? insurance,
            (Stream Stream, string FileName)? registration);
        Task<JetListingResponse> SaveStep6Async(JetStep6PilotRequest request);
        Task<JetListingResponse> SaveStep7Async(JetStep7AvailabilityRequest request);
        Task<JetListingResponse> SaveStep8Async(JetStep8GoalsRequest request);
        Task<JetListing?> GetInProgressListingAsync(string userId);
        Task<JetFullDataResponse?> GetListingFullDataAsync(Guid listingId);
        Task<IEnumerable<JetListing>> GetMyListingsAsync(string userId);
    }
}