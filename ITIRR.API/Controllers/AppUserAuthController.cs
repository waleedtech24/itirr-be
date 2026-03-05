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
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.RegisterAsync(request);

                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .Fail(result.Message));

                return Ok(ApiResponse<AppUserAuthResponse>
                    .Created(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/user/auth/verify-otp
        [HttpPost("verify-otp")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> VerifyOtp(
            [FromBody] AppUserVerifyOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.VerifyOtpAsync(request);

                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .Fail(result.Message));

                return Ok(ApiResponse<AppUserAuthResponse>
                    .Success(result, "OTP verified successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/user/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> Login(
            [FromBody] AppUserLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.LoginAsync(request);

                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .Fail(result.Message));

                return Ok(ApiResponse<AppUserAuthResponse>
                    .Success(result, "Login successful."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/user/auth/resend-otp
        [HttpPost("resend-otp")]
        public async Task<ActionResult<ApiResponse<AppUserAuthResponse>>> ResendOtp(
            [FromBody] AppUserResendOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .ValidationFailed("Invalid request data."));

                var result = await _service.ResendOtpAsync(request.EmailOrPhone);

                if (!result.Success)
                    return BadRequest(ApiResponse<AppUserAuthResponse>
                        .Fail(result.Message));

                return Ok(ApiResponse<AppUserAuthResponse>
                    .Success(result, "OTP resent successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AppUserAuthResponse>
                    .ServerError(ex.Message));
            }
        }
    }

    public class AppUserResendOtpRequest
    {
        public string EmailOrPhone { get; set; } = string.Empty;
    }
}