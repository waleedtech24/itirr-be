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

        // GET api/v1/user/profile
        [HttpGet]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> GetProfile()
        {
            try
            {
                var result = await _service.GetProfileAsync(GetUserId());

                if (result == null)
                    return NotFound(ApiResponse<AppUserProfileResponse>
                        .NotFound("No Record Found"));

                return Ok(ApiResponse<AppUserProfileResponse>
                    .Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/user/profile/basic
        [HttpPut("basic")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UpdateBasic(
            [FromBody] AppUserUpdateBasicRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserProfileResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.UpdateBasicInfoAsync(GetUserId(), request);

                return Ok(ApiResponse<AppUserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // PUT api/v1/user/profile/contact
        [HttpPut("contact")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UpdateContact(
            [FromBody] AppUserUpdateContactRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserProfileResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.UpdateContactAsync(GetUserId(), request);

                return Ok(ApiResponse<AppUserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/user/profile/photo
        [HttpPost("photo")]
        public async Task<ActionResult<ApiResponse<AppUserProfileResponse>>> UploadPhoto(
            IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                    return BadRequest(ApiResponse<AppUserProfileResponse>
                        .ValidationFailed("No photo provided."));

                using var stream = photo.OpenReadStream();
                var result = await _service.UploadPhotoAsync(
                    GetUserId(), stream, photo.FileName);

                return Ok(ApiResponse<AppUserProfileResponse>
                    .Updated(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserProfileResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/user/profile/documents
        [HttpPost("documents")]
        public async Task<ActionResult<ApiResponse<AppUserDocumentResponse>>> UploadDocument(
            IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(ApiResponse<AppUserDocumentResponse>
                        .ValidationFailed("No file provided."));

                using var stream = file.OpenReadStream();
                var result = await _service.UploadDocumentAsync(
                    GetUserId(), stream, file.FileName, file.Length);

                return Ok(ApiResponse<AppUserDocumentResponse>
                    .Created(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserDocumentResponse>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/user/profile/documents
        [HttpGet("documents")]
        public async Task<ActionResult<ApiResponse<List<AppUserDocumentResponse>>>> GetDocuments()
        {
            try
            {
                var result = await _service.GetDocumentsAsync(GetUserId());

                if (result == null || !result.Any())
                    return Ok(ApiResponse<List<AppUserDocumentResponse>>
                        .NotFound("No documents found."));

                return Ok(ApiResponse<List<AppUserDocumentResponse>>
                    .Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<AppUserDocumentResponse>>
                    .ServerError(ex.Message));
            }
        }

        // DELETE api/v1/user/profile/documents/{id}
        [HttpDelete("documents/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDocument(Guid id)
        {
            try
            {
                var result = await _service.DeleteDocumentAsync(GetUserId(), id);

                if (!result)
                    return NotFound(ApiResponse<bool>
                        .NotFound("Document not found."));

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