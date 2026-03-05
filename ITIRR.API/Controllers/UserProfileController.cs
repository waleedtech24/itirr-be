using ITIRR.Core.DTOs.Profile;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/profile")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;

        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        // GET api/v1/profile
        [HttpGet]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> GetProfile()
        {
            try
            {
                var profile = await _profileService.GetProfileAsync(GetUserId());

                if (profile == null)
                    return Ok(ApiResponse<UserProfileResponse>
                        .NotFound("No profile found."));

                return Ok(ApiResponse<UserProfileResponse>
                    .Success(profile));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/profile/basic
        [HttpPut("basic")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UpdateBasic(
            [FromBody] UpdateBasicInfoRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<UserProfileResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _profileService.UpdateBasicInfoAsync(request, GetUserId());

                return Ok(ApiResponse<UserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/profile/contact
        [HttpPut("contact")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UpdateContact(
            [FromBody] UpdateContactRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<UserProfileResponse>
                        .ValidationFailed(string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))));

                var result = await _profileService.UpdateContactAsync(request, GetUserId());

                return Ok(ApiResponse<UserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/profile/photo
        [HttpPost("photo")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UploadPhoto(
            IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                    return BadRequest(ApiResponse<UserProfileResponse>
                        .ValidationFailed("No photo provided."));

                var result = await _profileService.UpdateProfilePhotoAsync(
                    photo.OpenReadStream(), photo.FileName, GetUserId());

                return Ok(ApiResponse<UserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/profile/documents
        [HttpGet("documents")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDocumentResponse>>>> GetDocuments()
        {
            try
            {
                var docs = await _profileService.GetDocumentsAsync(GetUserId());

                if (docs == null || !docs.Any())
                    return Ok(ApiResponse<IEnumerable<UserDocumentResponse>>
                        .NotFound("No documents found."));

                return Ok(ApiResponse<IEnumerable<UserDocumentResponse>>
                    .Success(docs));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<UserDocumentResponse>>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/profile/documents
        [HttpPost("documents")]
        public async Task<ActionResult<ApiResponse<UserDocumentResponse>>> UploadDocument(
            IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(ApiResponse<UserDocumentResponse>
                        .ValidationFailed("No file provided."));

                var result = await _profileService.UploadDocumentAsync(
                    file.OpenReadStream(), file.FileName, file.Length, GetUserId());

                return Ok(ApiResponse<UserDocumentResponse>
                    .Created(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserDocumentResponse>
                    .ServerError(ex.Message));
            }
        }

        // DELETE api/v1/profile/documents/{id}
        [HttpDelete("documents/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDocument(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(ApiResponse<bool>
                        .ValidationFailed("Document ID is required."));

                await _profileService.DeleteDocumentAsync(id, GetUserId());

                return Ok(ApiResponse<bool>
                    .Deleted());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>
                    .ServerError(ex.Message));
            }
        }
    }
}