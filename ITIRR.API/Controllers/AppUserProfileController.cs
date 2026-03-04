using ITIRR.Core.DTOs.User;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/user/profile")]
    [ApiController]
    [Authorize]
    public class AppUserProfileController : ControllerBase
    {
        private readonly IAppUserProfileService _service;

        public AppUserProfileController(IAppUserProfileService service) =>
            _service = service;

        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> GetProfile()
        {
            var result = await _service.GetProfileAsync(GetUserId());
            if (result == null)
                return NotFound(ApiResponse<AppUserProfileResponse>
                    .ErrorResponse("User not found."));
            return Ok(ApiResponse<AppUserProfileResponse>
                .SuccessResponse(result, "Profile loaded."));
        }

        [HttpPut("basic")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UpdateBasic(
            [FromBody] AppUserUpdateBasicRequest request)
        {
            var result = await _service.UpdateBasicInfoAsync(GetUserId(), request);
            return Ok(ApiResponse<AppUserProfileResponse>
                .SuccessResponse(result, "Basic info updated."));
        }

        [HttpPut("contact")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UpdateContact(
            [FromBody] AppUserUpdateContactRequest request)
        {
            var result = await _service.UpdateContactAsync(GetUserId(), request);
            return Ok(ApiResponse<AppUserProfileResponse>
                .SuccessResponse(result, "Contact info updated."));
        }

        [HttpPost("photo")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UploadPhoto(
            IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest(ApiResponse<AppUserProfileResponse>
                    .ErrorResponse("No photo provided."));

            using var stream = photo.OpenReadStream();
            var result = await _service.UploadPhotoAsync(
                GetUserId(), stream, photo.FileName);
            return Ok(ApiResponse<AppUserProfileResponse>
                .SuccessResponse(result, "Photo uploaded."));
        }

        [HttpPost("documents")]
        public async Task<ActionResult<ApiResponse<AppUserDocumentResponse>>> UploadDocument(
            IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(ApiResponse<AppUserDocumentResponse>
                    .ErrorResponse("No file provided."));

            using var stream = file.OpenReadStream();
            var result = await _service.UploadDocumentAsync(
                GetUserId(), stream, file.FileName, file.Length);
            return Ok(ApiResponse<AppUserDocumentResponse>
                .SuccessResponse(result, "Document uploaded."));
        }

        [HttpGet("documents")]
        public async Task<ActionResult<ApiResponse<List<AppUserDocumentResponse>>>> GetDocuments()
        {
            var result = await _service.GetDocumentsAsync(GetUserId());
            return Ok(ApiResponse<List<AppUserDocumentResponse>>
                .SuccessResponse(result, "Documents loaded."));
        }

        [HttpDelete("documents/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDocument(Guid id)
        {
            var result = await _service.DeleteDocumentAsync(GetUserId(), id);
            if (!result)
                return NotFound(ApiResponse<bool>.ErrorResponse("Document not found."));
            return Ok(ApiResponse<bool>.SuccessResponse(true, "Document deleted."));
        }
    }
}