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

        // POST api/v1/vehicle-listing/step1
        [HttpPost("step1")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step1(
            [FromBody] Step1LocationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep1Async(request, GetUserId());
                return Ok(ApiResponse<VehicleListingResponse>
                    .Created(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step2
        [HttpPost("step2")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step2(
            [FromBody] Step2VINRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep2Async(request);
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step3
        [HttpPost("step3")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step3(
            [FromBody] Step3OdometerRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep3Async(request);
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step4
        [HttpPost("step4")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step4(
            [FromBody] Step4HistoryRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep4Async(request);
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step5
        [HttpPost("step5")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step5(
            [FromBody] Step5GoalsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep5Async(request);
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step6
        [HttpPost("step6")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step6(
            [FromBody] Step6AvailabilityRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep6Async(request);
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step7
        [HttpPost("step7")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step7(
            [FromForm] Guid listingId,
            IFormFileCollection? interiorPhotos,
            IFormFileCollection? exteriorPhotos)
        {
            try
            {
                if (listingId == Guid.Empty)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed("Listing ID is required."));

                var interior = (interiorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var exterior = (exteriorPhotos ?? new FormFileCollection())
                    .Select(f => (f.OpenReadStream(), f.FileName)).ToList();

                var result = await _listingService.SaveStep7PhotosAsync(
                    listingId, interior, exterior);

                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step8
        [HttpPost("step8")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> Step8(
            [FromBody] Step8DriverLicenceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveStep8Async(request, GetUserId());
                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/vehicle-listing/step9
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
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

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

                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/vehicle-listing/my-listings
        [HttpGet("my-listings")]
        public async Task<ActionResult<ApiResponse<IEnumerable<VehicleListingResponse>>>> GetMyListings()
        {
            try
            {
                var listings = await _listingService.GetMyListingsAsync(GetUserId());

                if (listings == null || !listings.Any())
                    return Ok(ApiResponse<IEnumerable<VehicleListingResponse>>
                        .NotFound("No listings found."));

                var response = listings.Select(l => new VehicleListingResponse
                {
                    ListingId = l.Id,
                    VehicleType = l.VehicleType ?? "car",
                    Status = l.Status,
                    CurrentStep = l.CurrentStep,
                    Message = "Success",
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

                return Ok(ApiResponse<IEnumerable<VehicleListingResponse>>
                    .Success(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<VehicleListingResponse>>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/vehicle-listing/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> GetById(Guid id)
        {
            try
            {
                var listing = await _listingService.GetListingByIdAsync(id);

                if (listing == null)
                    return NotFound(ApiResponse<VehicleListingResponse>
                        .NotFound("Listing not found."));

                var response = new VehicleListingResponse
                {
                    ListingId = listing.Id,
                    VehicleType = listing.VehicleType,
                    Status = listing.Status,
                    CurrentStep = listing.CurrentStep,
                    Message = "Success"
                };

                return Ok(ApiResponse<VehicleListingResponse>
                    .Success(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/vehicle-listing/in-progress
        [HttpGet("in-progress")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetInProgress()
        {
            try
            {
                var listing = await _listingService.GetInProgressListingAsync(GetUserId());

                if (listing == null)
                    return Ok(ApiResponse<ListingFullDataResponse>
                        .NotFound("No in-progress listing found."));

                var data = await _listingService.GetListingFullDataAsync(listing.Id);

                if (data == null)
                    return Ok(ApiResponse<ListingFullDataResponse>
                        .NotFound("Listing data not found."));

                return Ok(ApiResponse<ListingFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/vehicle-listing/{id}/full-data
        [HttpGet("{id}/full-data")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetFullData(Guid id)
        {
            try
            {
                var data = await _listingService.GetListingFullDataAsync(id);

                if (data == null)
                    return NotFound(ApiResponse<ListingFullDataResponse>
                        .NotFound("Listing not found."));

                return Ok(ApiResponse<ListingFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/vehicle-listing/{id}/edit-data
        [HttpGet("{id}/edit-data")]
        public async Task<ActionResult<ApiResponse<ListingFullDataResponse>>> GetEditData(Guid id)
        {
            try
            {
                var data = await _listingService.GetListingFullDataAsync(id);

                if (data == null)
                    return NotFound(ApiResponse<ListingFullDataResponse>
                        .NotFound("Listing not found."));

                return Ok(ApiResponse<ListingFullDataResponse>
                    .Success(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ListingFullDataResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/vehicle-listing/{id}/save
        [HttpPut("{id}/save")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> SaveEdit(
            Guid id, [FromBody] EditListingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveEditAsync(
                    id, request, GetUserId(), submit: false);

                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/vehicle-listing/{id}/save-and-submit
        [HttpPut("{id}/save-and-submit")]
        public async Task<ActionResult<ApiResponse<VehicleListingResponse>>> SaveAndSubmit(
            Guid id, [FromBody] EditListingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<VehicleListingResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _listingService.SaveEditAsync(
                    id, request, GetUserId(), submit: true);

                return Ok(ApiResponse<VehicleListingResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<VehicleListingResponse>
                    .ServerError(ex.Message));
            }
        }
    }
}