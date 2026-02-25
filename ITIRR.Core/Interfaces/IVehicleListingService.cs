using ITIRR.Core.DTOs.VehicleListing;
using ITIRR.Core.Entities;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleListingService
    {
        Task<VehicleListingResponse> SaveStep1Async(Step1LocationRequest request, string userId);
        Task<VehicleListingResponse> SaveStep2Async(Step2VINRequest request);
        Task<VehicleListingResponse> SaveStep3Async(Step3OdometerRequest request);
        Task<VehicleListingResponse> SaveStep4Async(Step4HistoryRequest request);
        Task<VehicleListingResponse> SaveStep5Async(Step5GoalsRequest request);
        Task<VehicleListingResponse> SaveStep6Async(Step6AvailabilityRequest request);

        Task<VehicleListingResponse> SaveStep7PhotosAsync(
            Guid listingId,
            List<(Stream Stream, string FileName)> interiorPhotos,
            List<(Stream Stream, string FileName)> exteriorPhotos);

        Task<VehicleListingResponse> SaveStep8Async(Step8DriverLicenceRequest request, string userId);

        Task<VehicleListingResponse> SaveStep9Async(
            Step9PCORequest request,
            (Stream Stream, string FileName)? roadTax,
            (Stream Stream, string FileName)? mot,
            (Stream Stream, string FileName)? roadTax2,
            (Stream Stream, string FileName)? logbook,
            string userId);

        Task<VehicleListing?> GetListingByIdAsync(Guid id);
        Task<IEnumerable<VehicleListing>> GetMyListingsAsync(string userId);
        Task<VehicleListing?> GetInProgressListingAsync(string userId);
        Task<ListingFullDataResponse?> GetListingFullDataAsync(Guid listingId);
    }
}