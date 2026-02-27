using ITIRR.Core.DTOs.VehicleListing;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIRR.API.Controllers
{
    public class Step7FormRequest
    {
        public Guid ListingId { get; set; }
    }

    public class Step9FormRequest
    {
        public Guid ListingId { get; set; }
        public string VehicleMakeModel { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleColor { get; set; } = string.Empty;
        public string YearOfManufacture { get; set; } = string.Empty;
        public string PHVLicenceNumber { get; set; } = string.Empty;
        public string PHVLicenceExpiryDate { get; set; } = string.Empty;
    }

    [Route("api/v1/vehicle-listing")]
    [ApiController]
    [Authorize]
    public class VehicleListingController : ControllerBase
    {
        private readonly IVehicleListingService _listingService;

        public VehicleListingController(IVehicleListingService listingService)
        {
            _listingService = listingService;
        }

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step1(
            [FromBody] Step1LocationRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep1Async(request, GetUserId());
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step2(
            [FromBody] Step2VINRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep2Async(request);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step3")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step3(
            [FromBody] Step3OdometerRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep3Async(request);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step4")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step4(
            [FromBody] Step4HistoryRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep4Async(request);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step5")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step5(
            [FromBody] Step5GoalsRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep5Async(request);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step6")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step6(
            [FromBody] Step6AvailabilityRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep6Async(request);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step7")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step7(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                var interior = (interiorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName))
                    .ToList();

                var exterior = (exteriorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName))
                    .ToList();

                var result = await _listingService.SaveStep7PhotosAsync(
                    listingId, interior, exterior);

                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step8")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step8(
            [FromBody] Step8DriverLicenceRequest request)
        {
            try
            {
                var result = await _listingService.SaveStep8Async(request, GetUserId());
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPost("step9")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step9(
            [FromForm] Step9FormRequest request,
            IFormFile? roadTax,
            IFormFile? mot,
            IFormFile? roadTax2,
            IFormFile? logbook)
        {
            try
            {
                var pcoRequest = new Step9PCORequest
                {
                    ListingId = request.ListingId,
                    VehicleMakeModel = request.VehicleMakeModel,
                    PlateNumber = request.PlateNumber,
                    VehicleColor = request.VehicleColor,
                    YearOfManufacture = request.YearOfManufacture,
                    PHVLicenceNumber = request.PHVLicenceNumber,
                    PHVLicenceExpiryDate = request.PHVLicenceExpiryDate
                };

                var result = await _listingService.SaveStep9Async(
                    pcoRequest,
                    roadTax != null ? (roadTax.OpenReadStream(), roadTax.FileName) : null,
                    mot != null ? (mot.OpenReadStream(), mot.FileName) : null,
                    roadTax2 != null ? (roadTax2.OpenReadStream(), roadTax2.FileName) : null,
                    logbook != null ? (logbook.OpenReadStream(), logbook.FileName) : null,
                    GetUserId());

                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpGet("my-listings")]
        public async Task<ActionResult<ApiResponse<IEnumerable<VehicleListingResponse>>>> GetMyListings()
        {
            try
            {
                var listings = await _listingService.GetMyListingsAsync(GetUserId());

                var response = listings.Select(l => new VehicleListingResponse
                {
                    ListingId = l.Id,
                    VehicleType = l.VehicleType ?? "car",
                    Status = l.Status,
                    CurrentStep = l.CurrentStep,
                    Message = "Success.",
                    LicencePlateNumber = l.LicencePlateNumber,
                    VIN = l.VIN,
                    StreetAddress = l.StreetAddress,
                    State = l.State,
                    ZipCode = l.ZipCode,
                    OdometerReading = l.OdometerReading,
                    Transmission = l.Transmission,
                    PrimaryGoal = l.PrimaryGoal,
                    FirstPhotoUrl = l.Media != null && l.Media.Any(m => m.MediaType == "Exterior")
                                        ? l.Media.First(m => m.MediaType == "Exterior").MediaUrl
                                        : l.Media != null && l.Media.Any()
                                        ? l.Media.First().MediaUrl
                                        : null,
                    CreatedAt = l.CreatedAt
                });

                return Ok(ApiResponse<IEnumerable<VehicleListingResponse>>.SuccessResponse(
                    response, "Listings retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<VehicleListingResponse>>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> GetById(Guid id)
        {
            try
            {
                var listing = await _listingService.GetListingByIdAsync(id);
                if (listing == null)
                    return NotFound(ApiResponse<VehicleListingResponse>.ErrorResponse(
                        "Listing not found", new List<string> { "Not found" }));

                var response = new VehicleListingResponse
                {
                    ListingId = listing.Id,
                    VehicleType = listing.VehicleType,
                    Status = listing.Status,
                    CurrentStep = listing.CurrentStep,
                    Message = "Success"
                };

                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(
                    response, "Listing retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _listingService.GetInProgressListingAsync(GetUserId());
                if (listing == null)
                    return Ok(ApiResponse<ListingFullDataResponse>.SuccessResponse(null!, "No in-progress listing"));

                var data = await _listingService.GetListingFullDataAsync(listing.Id);
                return Ok(ApiResponse<ListingFullDataResponse>.SuccessResponse(data!, "Found"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}/full-data")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetFullData(Guid id)
        {
            try
            {
                var data = await _listingService.GetListingFullDataAsync(id);
                if (data == null)
                    return NotFound(ApiResponse<ListingFullDataResponse>.ErrorResponse(
                        "Not found", new List<string> { "Not found" }));

                return Ok(ApiResponse<ListingFullDataResponse>.SuccessResponse(data, "Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}/edit-data")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetEditData(Guid id)
        {
            try
            {
                var data = await _listingService.GetListingFullDataAsync(id);
                if (data == null)
                    return NotFound(ApiResponse<ListingFullDataResponse>.ErrorResponse(
                        "Not found", new List<string> { "Not found" }));

                return Ok(ApiResponse<ListingFullDataResponse>.SuccessResponse(data, "Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}/save")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> SaveEdit(
            Guid id, [FromBody] EditListingRequest request)
        {
            try
            {
                var result = await _listingService.SaveEditAsync(id, request, GetUserId(), submit: false);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, "Saved as draft"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        [HttpPut("{id}/save-and-submit")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> SaveAndSubmit(
            Guid id, [FromBody] EditListingRequest request)
        {
            try
            {
                var result = await _listingService.SaveEditAsync(id, request, GetUserId(), submit: true);
                return Ok(ApiResponse<VehicleListingResponse>.SuccessResponse(result, "Submitted for review"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }
    }
}
