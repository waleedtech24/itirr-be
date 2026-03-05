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

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        // POST api/v1/boat-listing/step1
        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step1(
            [FromBody] BoatStep1LocationRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep1Async(req, GetUserId());
                return Ok(ApiResponse<BoatListingResponse>
                    .Created(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/boat-listing/step2
        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step2(
            [FromBody] BoatStep2DetailsRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep2Async(req);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/boat-listing/step3
        [HttpPost("step3")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step3(
            [FromBody] BoatStep3GoalsRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep3Async(req);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/boat-listing/step4
        [HttpPost("step4")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step4(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                if (listingId == Guid.Empty)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed("Listing ID is required."));

                var interior = (interiorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var exterior = (exteriorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();

                var r = await _service.SaveStep4PhotosAsync(listingId, interior, exterior);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/boat-listing/step5
        [HttpPost("step5")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step5(
            [FromBody] BoatStep5AvailabilityRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var r = await _service.SaveStep5Async(req);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/boat-listing/step6
        [HttpPost("step6")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> Step6(
            [FromForm] BoatStep6FormRequest req,
            IFormFile? licenceDoc)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

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

                var doc = licenceDoc != null
                    ? (licenceDoc.OpenReadStream(), licenceDoc.FileName)
                    : ((Stream, string)?)null;

                var r = await _service.SaveStep6Async(request, doc);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(r));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/boat-listing/in-progress
        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<BoatFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _service.GetInProgressListingAsync(GetUserId());

                if (listing == null)
                    return Ok(ApiResponse<BoatFullDataResponse>
                        .NotFound("No in-progress listing found."));

                var data = await _service.GetListingFullDataAsync(listing.Id);

                if (data == null)
                    return Ok(ApiResponse<BoatFullDataResponse>
                        .NotFound("Listing data not found."));

                return Ok(ApiResponse<BoatFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/boat-listing/my-listings
        [HttpGet("my-listings")]
        public async Task<ActionResult<ApiResponse<List<BoatListingResponse>>>> GetMyListings()
        {
            try
            {
                var listings = await _service.GetMyListingsAsync(GetUserId());

                if (listings == null || !listings.Any())
                    return Ok(ApiResponse<List<BoatListingResponse>>
                        .NotFound("No listings found."));

                var result = listings.Select(b => new BoatListingResponse
                {
                    ListingId = b.Id,
                    Status = b.Status,
                    CurrentStep = b.CurrentStep,
                    State = b.State,
                    BoatType = b.BoatType,
                    BoatMake = b.BoatMake,
                    BoatModel = b.BoatModel,
                    BoatYear = b.BoatYear,
                    BoatLength = b.BoatLength,
                    PassengerCapacity = b.PassengerCapacity,
                    RegistrationNumber = b.RegistrationNumber,
                    PrimaryGoal = b.PrimaryGoal,
                    FirstPhotoUrl = b.Media != null && b.Media.Any()
                                            ? b.Media.OrderBy(m => m.DisplayOrder)
                                                     .First().MediaUrl
                                            : null,
                    CreatedAt = b.CreatedAt,
                    Message = "Success"
                }).ToList();

                return Ok(ApiResponse<List<BoatListingResponse>>
                    .Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<BoatListingResponse>>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/boat-listing/{id}/edit-data
        [HttpGet("{id}/edit-data")]
        public async Task<ActionResult<ApiResponse<BoatFullDataResponse>>> GetEditData(Guid id)
        {
            try
            {
                var data = await _service.GetListingFullDataAsync(id);

                if (data == null)
                    return NotFound(ApiResponse<BoatFullDataResponse>
                        .NotFound("Listing not found."));

                return Ok(ApiResponse<BoatFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/boat-listing/{id}/save
        [HttpPut("{id}/save")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> SaveEdit(
            Guid id, [FromBody] BoatEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _service.SaveEditAsync(id, request, GetUserId(), submit: false);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/boat-listing/{id}/save-and-submit
        [HttpPut("{id}/save-and-submit")]
        public async Task<ActionResult<ApiResponse<BoatListingResponse>>> SaveAndSubmit(
            Guid id, [FromBody] BoatEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<BoatListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _service.SaveEditAsync(id, request, GetUserId(), submit: true);
                return Ok(ApiResponse<BoatListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BoatListingResponse>
                    .ServerError(ex.Message));
            }
        }
    }
}