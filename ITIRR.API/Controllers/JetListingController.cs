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

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        // POST api/v1/jet-listing/step1
        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step1(
            [FromBody] JetStep1LocationRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep1Async(req, GetUserId());
                return Ok(ApiResponse<JetListingResponse>
                    .Created(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step2
        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step2(
            [FromBody] JetStep2SpecsRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep2Async(req);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step3
        [HttpPost("step3")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step3(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                if (listingId == Guid.Empty)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed("Listing ID is required."));

                var interior = (interiorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var exterior = (exteriorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();

                var r = await _service.SaveStep3PhotosAsync(listingId, interior, exterior);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step4
        [HttpPost("step4")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step4(
            [FromBody] JetStep4CabinRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep4Async(req);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step5
        [HttpPost("step5")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step5(
            [FromForm] JetStep5FormRequest req,
            IFormFile? airworthiness,
            IFormFile? insurance,
            IFormFile? registration)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var request = new JetStep5SafetyRequest
                {
                    ListingId = req.ListingId,
                    LastMaintenanceDate = DateTime.Parse(req.LastMaintenanceDate),
                    SafetyCertifications = req.SafetyCertifications
                };

                var r = await _service.SaveStep5Async(request,
                    airworthiness != null
                        ? (airworthiness.OpenReadStream(), airworthiness.FileName)
                        : null,
                    insurance != null
                        ? (insurance.OpenReadStream(), insurance.FileName)
                        : null,
                    registration != null
                        ? (registration.OpenReadStream(), registration.FileName)
                        : null);

                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step6
        [HttpPost("step6")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step6(
            [FromBody] JetStep6PilotRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep6Async(req);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step7
        [HttpPost("step7")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step7(
            [FromBody] JetStep7AvailabilityRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep7Async(req);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/jet-listing/step8
        [HttpPost("step8")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> Step8(
            [FromBody] JetStep8GoalsRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep8Async(req);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/jet-listing/in-progress
        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<JetFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _service.GetInProgressListingAsync(GetUserId());

                if (listing == null)
                    return Ok(ApiResponse<JetFullDataResponse>
                        .NotFound("No in-progress listing found."));

                var data = await _service.GetListingFullDataAsync(listing.Id);

                if (data == null)
                    return Ok(ApiResponse<JetFullDataResponse>
                        .NotFound("Listing data not found."));

                return Ok(ApiResponse<JetFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/jet-listing/my-listings
        [HttpGet("my-listings")]
        public async Task<ActionResult<ApiResponse<List<JetListingResponse>>>> GetMyListings()
        {
            try
            {
                var listings = await _service.GetMyListingsAsync(GetUserId());

                if (listings == null || !listings.Any())
                    return Ok(ApiResponse<List<JetListingResponse>>
                        .NotFound("No listings found."));

                var result = listings.Select(j => new JetListingResponse
                {
                    ListingId = j.Id,
                    Status = j.Status,
                    CurrentStep = j.CurrentStep,
                    State = j.State,
                    AircraftMake = j.AircraftMake,
                    AircraftModel = j.AircraftModel,
                    AircraftYear = j.AircraftYear,
                    TailNumber = j.TailNumber,
                    AircraftCategory = j.AircraftCategory,
                    PassengerCapacity = j.PassengerCapacity,
                    HomeAirport = j.HomeAirport,
                    HangarLocation = j.HangarLocation,
                    PrimaryGoal = j.PrimaryGoal,
                    FirstPhotoUrl = j.Media != null && j.Media.Any()
                                            ? j.Media.OrderBy(m => m.DisplayOrder)
                                                     .First().MediaUrl
                                            : null,
                    CreatedAt = j.CreatedAt,
                    Message = "Success"
                }).ToList();

                return Ok(ApiResponse<List<JetListingResponse>>
                    .Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<JetListingResponse>>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/jet-listing/{id}/edit-data
        [HttpGet("{id}/edit-data")]
        public async Task<ActionResult<ApiResponse<JetFullDataResponse>>> GetEditData(Guid id)
        {
            try
            {
                var data = await _service.GetListingFullDataAsync(id);

                if (data == null)
                    return NotFound(ApiResponse<JetFullDataResponse>
                        .NotFound("Listing not found."));

                return Ok(ApiResponse<JetFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/jet-listing/{id}/save
        [HttpPut("{id}/save")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> SaveEdit(
            Guid id, [FromBody] JetEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _service.SaveEditAsync(id, request, GetUserId(), submit: false);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/jet-listing/{id}/save-and-submit
        [HttpPut("{id}/save-and-submit")]
        public async Task<ActionResult<ApiResponse<JetListingResponse>>> SaveAndSubmit(
            Guid id, [FromBody] JetEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<JetListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _service.SaveEditAsync(id, request, GetUserId(), submit: true);
                return Ok(ApiResponse<JetListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<JetListingResponse>
                    .ServerError(ex.Message));
            }
        }
    }
}