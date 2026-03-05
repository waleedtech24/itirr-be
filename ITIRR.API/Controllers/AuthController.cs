using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ITIRR.Services.DTOs.Auth;
using ITIRR.Services.DTOs.Common;
using ITIRR.Core.DTOs.Auth;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/v1/auth/register-partner
        [HttpPost("register-partner")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> RegisterPartner(
            [FromBody] RegisterPartnerRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<RegisterResponse>
                        .ValidationFailed(
                            string.Join(", ", ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage))));

                var result = await _authService.RegisterPartnerAsync(request);

                if (!result.OtpSent)
                    return BadRequest(ApiResponse<RegisterResponse>
                        .Fail("Registration failed. Could not send OTP."));

                return Ok(ApiResponse<RegisterResponse>
                    .Created(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<RegisterResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/auth/send-otp
        [HttpPost("send-otp")]
        public async Task<ActionResult<ApiResponse<OtpResponse>>> SendOtp(
            [FromBody] SendOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<OtpResponse>
                        .ValidationFailed(
                            string.Join(", ", ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage))));

                var result = await _authService.SendOtpAsync(request);

                if (!result.OtpSent)
                    return BadRequest(ApiResponse<OtpResponse>
                        .Fail(result.Message));

                return Ok(ApiResponse<OtpResponse>
                    .Success(result, "OTP sent successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<OtpResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/auth/verify-otp
        [HttpPost("verify-otp")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> VerifyOtp(
            [FromBody] VerifyOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AuthResponse>
                        .ValidationFailed(
                            string.Join(", ", ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage))));

                var result = await _authService.VerifyOtpAsync(request);

                if (string.IsNullOrEmpty(result.AccessToken))
                    return BadRequest(ApiResponse<AuthResponse>
                        .Fail("Invalid or expired OTP."));

                return Ok(ApiResponse<AuthResponse>
                    .Success(result, "OTP verified successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AuthResponse>
                    .ServerError(ex.Message));
            }
        }

        // POST api/v1/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login(
            [FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AuthResponse>
                        .ValidationFailed(
                            string.Join(", ", ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage))));

                var result = await _authService.LoginAsync(request);

                if (string.IsNullOrEmpty(result.AccessToken))
                    return BadRequest(ApiResponse<AuthResponse>
                        .Fail("Invalid credentials."));

                return Ok(ApiResponse<AuthResponse>
                    .Success(result, "Login successful."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AuthResponse>
                    .ServerError(ex.Message));
            }
        }
    }
}