using ITIRR.Core.DTOs.JetListing;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIRR.API.Controllers
{
    public class JetStep5FormRequest
    {
        public Guid ListingId { get; set; }
        public string LastMaintenanceDate { get; set; } = string.Empty;
        public List<string> SafetyCertifications { get; set; } = new();
    }

    [Route("api/v1/jet-listing")]
    [ApiController]
    [Authorize]
    public class JetListingController : ControllerBase
    {
        private readonly IJetListingService _service;
        public JetListingController(IJetListingService service) => _service = service;
        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step1([FromBody] JetStep1LocationRequest req)
        {
            try { var r = await _service.SaveStep1Async(req, GetUserId()); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step2([FromBody] JetStep2SpecsRequest req)
        {
            try { var r = await _service.SaveStep2Async(req); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step3")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step3(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                var interior = (interiorPhotos ?? new FormFileCollection()).Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var exterior = (exteriorPhotos ?? new FormFileCollection()).Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var r = await _service.SaveStep3PhotosAsync(listingId, interior, exterior);
                return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step4")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step4([FromBody] JetStep4CabinRequest req)
        {
            try { var r = await _service.SaveStep4Async(req); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step5")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step5(
            [FromForm] JetStep5FormRequest req,
            IFormFile? airworthiness, IFormFile? insurance, IFormFile? registration)
        {
            try
            {
                var request = new JetStep5SafetyRequest
                {
                    ListingId = req.ListingId,
                    LastMaintenanceDate = DateTime.Parse(req.LastMaintenanceDate),
                    SafetyCertifications = req.SafetyCertifications
                };
                var r = await _service.SaveStep5Async(request,
                    airworthiness != null ? (airworthiness.OpenReadStream(), airworthiness.FileName) : null,
                    insurance != null ? (insurance.OpenReadStream(), insurance.FileName) : null,
                    registration != null ? (registration.OpenReadStream(), registration.FileName) : null);
                return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step6")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step6([FromBody] JetStep6PilotRequest req)
        {
            try { var r = await _service.SaveStep6Async(req); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step7")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step7([FromBody] JetStep7AvailabilityRequest req)
        {
            try { var r = await _service.SaveStep7Async(req); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpPost("step8")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step8([FromBody] JetStep8GoalsRequest req)
        {
            try { var r = await _service.SaveStep8Async(req); return Ok(ApiResponse<JetListingResponse>.SuccessResponse(r, r.Message)); }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetListingResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }

        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<JetFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _service.GetInProgressListingAsync(GetUserId());
                if (listing == null) return Ok(ApiResponse<JetFullDataResponse>.SuccessResponse(null!, "No in-progress listing"));
                var data = await _service.GetListingFullDataAsync(listing.Id);
                return Ok(ApiResponse<JetFullDataResponse>.SuccessResponse(data!, "Found"));
            }
            catch (Exception ex) { return StatusCode(500, ApiResponse<JetFullDataResponse>.ErrorResponse(ex.Message, new List<string> { ex.Message })); }
        }
    }
}