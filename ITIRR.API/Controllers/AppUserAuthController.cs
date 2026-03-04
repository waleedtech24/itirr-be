using ITIRR.Core.DTOs.User;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/user/auth")]
    [ApiController]
    public class AppUserAuthController : ControllerBase
    {
        private readonly IAppUserAuthService _service;

        public AppUserAuthController(IAppUserAuthService service) =>
            _service = service;

        // POST api/v1/user/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> Register(
            [FromBody] AppUserRegisterRequest request)
        {
            try
            {
                var result = await _service.RegisterAsync(request);
                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ErrorResponse(result.Message, result.Errors));
                return Ok(ApiResponse<AppUserAuthResponse>
                    .SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ErrorResponse(ex.Message, new List<string> { ex.Message }));
            }
        }

        // POST api/v1/user/auth/verify-otp
        [HttpPost("verify-otp")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> VerifyOtp(
            [FromBody] AppUserVerifyOtpRequest request)
        {
            try
            {
                var result = await _service.VerifyOtpAsync(request);
                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ErrorResponse(result.Message, result.Errors));
                return Ok(ApiResponse<AppUserAuthResponse>
                    .SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ErrorResponse(ex.Message, new List<string> { ex.Message }));
            }
        }

        // POST api/v1/user/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> Login(
            [FromBody] AppUserLoginRequest request)
        {
            try
            {
                var result = await _service.LoginAsync(request);
                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ErrorResponse(result.Message, result.Errors));
                return Ok(ApiResponse<AppUserAuthResponse>
                    .SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ErrorResponse(ex.Message, new List<string> { ex.Message }));
            }
        }

        // POST api/v1/user/auth/resend-otp
        [HttpPost("resend-otp")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> ResendOtp(
            [FromBody] AppUserResendOtpRequest request)
        {
            try
            {
                var result = await _service.ResendOtpAsync(request.EmailOrPhone);
                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ErrorResponse(result.Message, result.Errors));
                return Ok(ApiResponse<AppUserAuthResponse>
                    .SuccessResponse(result, result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ErrorResponse(ex.Message, new List<string> { ex.Message }));
            }
        }
    }

    // Simple request DTO for resend
    public class AppUserResendOtpRequest
    {
        public string EmailOrPhone { get; set; } = string.Empty;
    }
}