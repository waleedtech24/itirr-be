using ITIRR.Core.DTOs.User;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.API.Controllers
{
    [Route("api/app/listings")]
    [ApiController]
    public class AppUserListingController : ControllerBase
    {
        private readonly IAppUserListingService _listingService;

        public AppUserListingController(IAppUserListingService listingService)
        {
            _listingService = listingService;
        }

        // GET api/app/listings?type=car&location=miami&minPrice=100&page=1&pageSize=10
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AppUserListingSearchResponse>>> Search(
            [FromQuery] AppUserListingSearchRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserListingSearchResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var (items, totalCount) = await _listingService.SearchListingsAsync(request);

                if (!items.Any())
                    return Ok(ApiResponse<AppUserListingSearchResponse>
                        .NotFound("No listings found."));

                return Ok(ApiResponse<AppUserListingSearchResponse>.Success(
                    new AppUserListingSearchResponse
                    {
                        Items = items,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize,
                        TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
                    }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserListingSearchResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/app/listings/{id}
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AppUserListingDetailResponse>>> GetDetail(Guid id)
        {
            try
            {
                var result = await _listingService.GetListingDetailAsync(id);

                if (result == null)
                    return Ok(ApiResponse<AppUserListingDetailResponse>
                        .NotFound("Listing not found or not available."));

                return Ok(ApiResponse<AppUserListingDetailResponse>
                    .Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserListingDetailResponse>
                    .ServerError(ex.Message));
            }
        }
    }
}