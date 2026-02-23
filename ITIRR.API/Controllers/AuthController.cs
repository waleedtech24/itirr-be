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

        [HttpPost("register-partner")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> RegisterPartner(
            [FromBody] RegisterPartnerRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<RegisterResponse>.ErrorResponse(
                        "Validation failed",
                        ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage).ToList()
                    ));

                var result = await _authService.RegisterPartnerAsync(request);

                if (!result.OtpSent)
                    return BadRequest(ApiResponse<RegisterResponse>.ErrorResponse(
                        "Registration failed",
                        new List<string> { "Could not send OTP" }
                    ));

                return Ok(ApiResponse<RegisterResponse>.SuccessResponse(
                    result,
                    "Registration successful. OTP sent to your email/phone."
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<RegisterResponse>.ErrorResponse(
                    "An error occurred during registration",
                    new List<string> { ex.Message }
                ));
            }
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<ApiResponse<OtpResponse>>> SendOtp(
            [FromBody] SendOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<OtpResponse>.ErrorResponse(
                        "Validation failed",
                        ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage).ToList()
                    ));

                var result = await _authService.SendOtpAsync(request);

                if (!result.OtpSent)
                    return BadRequest(ApiResponse<OtpResponse>.ErrorResponse(
                        result.Message,
                        new List<string> { result.Message }
                    ));

                return Ok(ApiResponse<OtpResponse>.SuccessResponse(
                    result,
                    result.Message
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<OtpResponse>.ErrorResponse(
                    "An error occurred while sending OTP",
                    new List<string> { ex.Message }
                ));
            }
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> VerifyOtp(
            [FromBody] VerifyOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AuthResponse>.ErrorResponse(
                        "Validation failed",
                        ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage).ToList()
                    ));

                var result = await _authService.VerifyOtpAsync(request);

                if (string.IsNullOrEmpty(result.AccessToken))
                    return BadRequest(ApiResponse<AuthResponse>.ErrorResponse(
                        "OTP verification failed",
                        new List<string> { "Invalid or expired OTP" }
                    ));

                return Ok(ApiResponse<AuthResponse>.SuccessResponse(
                    result,
                    "OTP verified successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AuthResponse>.ErrorResponse(
                    "An error occurred during OTP verification",
                    new List<string> { ex.Message }
                ));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login(
            [FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponse<AuthResponse>.ErrorResponse(
                        "Validation failed",
                        ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage).ToList()
                    ));

                var result = await _authService.LoginAsync(request);

                if (string.IsNullOrEmpty(result.AccessToken))
                    return BadRequest(ApiResponse<AuthResponse>.ErrorResponse(
                        "Login failed", new List<string> { "Invalid credentials" }
                    ));

                return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, "Login successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AuthResponse>.ErrorResponse(
                    "Login failed", new List<string> { ex.Message }
                ));
            }
        }
    }
}