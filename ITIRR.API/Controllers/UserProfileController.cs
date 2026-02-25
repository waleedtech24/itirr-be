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

        // GET /api/v1/profile
        [HttpGet]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> GetProfile()
        {
            try
            {
                var profile = await _profileService.GetProfileAsync(GetUserId());
                if (profile == null)
                    return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(
                        new UserProfileResponse(), "No profile found"));

                return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(profile, "Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // PUT /api/v1/profile/basic
        [HttpPut("basic")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UpdateBasic(
            [FromBody] UpdateBasicInfoRequest request)
        {
            try
            {
                var result = await _profileService.UpdateBasicInfoAsync(request, GetUserId());
                return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(result, "Basic info updated"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // PUT /api/v1/profile/contact
        [HttpPut("contact")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UpdateContact(
            [FromBody] UpdateContactRequest request)
        {
            try
            {
                var result = await _profileService.UpdateContactAsync(request, GetUserId());
                return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(result, "Contact info updated"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // POST /api/v1/profile/photo
        [HttpPost("photo")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UploadPhoto(IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                    return BadRequest(ApiResponse<UserProfileResponse>.ErrorResponse(
                        "No file", new List<string> { "No file uploaded" }));

                var result = await _profileService.UpdateProfilePhotoAsync(
                    photo.OpenReadStream(), photo.FileName, GetUserId());

                return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(result, "Photo updated"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserProfileResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // GET /api/v1/profile/documents
        [HttpGet("documents")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDocumentResponse>>>> GetDocuments()
        {
            try
            {
                var docs = await _profileService.GetDocumentsAsync(GetUserId());
                return Ok(ApiResponse<IEnumerable<UserDocumentResponse>>.SuccessResponse(docs, "Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<UserDocumentResponse>>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // POST /api/v1/profile/documents
        [HttpPost("documents")]
        public async Task<ActionResult<ApiResponse<UserDocumentResponse>>> UploadDocument(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(ApiResponse<UserDocumentResponse>.ErrorResponse(
                        "No file", new List<string> { "No file uploaded" }));

                var result = await _profileService.UploadDocumentAsync(
                    file.OpenReadStream(), file.FileName, file.Length, GetUserId());

                return Ok(ApiResponse<UserDocumentResponse>.SuccessResponse(result, "Document uploaded"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<UserDocumentResponse>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }

        // DELETE /api/v1/profile/documents/{id}
        [HttpDelete("documents/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDocument(Guid id)
        {
            try
            {
                await _profileService.DeleteDocumentAsync(id, GetUserId());
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Document deleted"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(
                    ex.Message, new List<string> { ex.Message }));
            }
        }
    }
}