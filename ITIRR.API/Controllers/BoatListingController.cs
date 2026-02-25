using ITIRR.Core.DTOs.BoatListing;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIRR.API.Controllers
{
    public class BoatStep6FormRequest
    {
        public Guid ListingId { get; set; }
        public string SkipperFirstName { get; set; } = string.Empty;
        public string? SkipperMiddleName { get; set; }
        public string SkipperLastName { get; set; } = string.Empty;
        public string SkipperLicenceNumber { get; set; } = string.Empty;
        public string SkipperLicenceType { get; set; } = string.Empty;
        public string SkipperLicenceExpiry { get; set; } = string.Empty;
    }

    [Route("api/v1/boat-listing")]
    [ApiController]
    [Authorize]
    public class BoatListingController : ControllerBase
    {
        private readonly IBoatListingService _service;
        public BoatListingController(IBoatListingService service) => _service = service;
        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step1([FromBody] BoatStep1LocationRequest req)
        {
            try { var r = await _service.SaveStep1Async(req, GetUserId()); return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step2([FromBody] BoatStep2DetailsRequest req)
        {
            try { var r = await _service.SaveStep2Async(req); return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step3")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step3([FromBody] BoatStep3GoalsRequest req)
        {
            try { var r = await _service.SaveStep3Async(req); return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step4")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step4(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                var interior = (interiorPhotos ?? new FormFileCollection()).Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var exterior = (exteriorPhotos ?? new FormFileCollection()).Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var r = await _service.SaveStep4PhotosAsync(listingId, interior, exterior);
                return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step5")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step5([FromBody] BoatStep5AvailabilityRequest req)
        {
            try { var r = await _service.SaveStep5Async(req); return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step6")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step6(
            [FromForm] BoatStep6FormRequest req,
            IFormFile? licenceDoc)
        {
            try
            {
                var request = new BoatStep6SkipperRequest
                {
                    ListingId = req.ListingId,
                    SkipperFirstName = req.SkipperFirstName,
                    SkipperMiddleName = req.SkipperMiddleName,
                    SkipperLastName = req.SkipperLastName,
                    SkipperLicenceNumber = req.SkipperLicenceNumber,
                    SkipperLicenceType = req.SkipperLicenceType,
                    SkipperLicenceExpiry = DateTime.Parse(req.SkipperLicenceExpiry)
                };
                var doc = licenceDoc != null ? (licenceDoc.OpenReadStream(), licenceDoc.FileName) : ((Stream, string)?)null;
                var r = await _service.SaveStep6Async(request, doc);
                return Ok(ApiResponse<BoatListingResponse>.SuccessResponse(r, r.Message));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<BoatFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _service.GetInProgressListingAsync(GetUserId());
                if (listing == null) return Ok(ApiResponse<BoatFullDataResponse>.SuccessResponse(null!, "No in-progress listing"));
                var data = await _service.GetListingFullDataAsync(listing.Id);
                return Ok(ApiResponse<BoatFullDataResponse>.SuccessResponse(data!, "Found"));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<BoatFullDataResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }
    }
}