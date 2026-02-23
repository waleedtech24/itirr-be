using ITIRR.Core.DTOs.Auth;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITIRR.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOtpService _otpService;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOtpService otpService,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _otpService = otpService;
            _tokenService = tokenService;
        }

        public async Task<RegisterResponse> RegisterPartnerAsync(RegisterPartnerRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.EmailOrPhone);
            if (existingUser != null)
                throw new Exception("User with this email already exists");

            var user = new ApplicationUser
            {
                UserName = request.EmailOrPhone,
                Email = request.EmailOrPhone,
                AgencyName = request.AgencyName, 
                UserType = "Host",
                AccountType = "Agency",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsVerified = false
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            var otpCode = _otpService.GenerateOtp();
            user.OtpCode = otpCode;
            user.OtpExpiresAt = DateTime.UtcNow.AddMinutes(10);
            await _userManager.UpdateAsync(user);

            await _otpService.SendOtpAsync(request.EmailOrPhone, otpCode, request.ContactType);

            return new RegisterResponse
            {
                UserId = user.Id,
                EmailOrPhone = request.EmailOrPhone,
                OtpSent = true,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10)
            };
        }

        public async Task<OtpResponse> SendOtpAsync(SendOtpRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone);
            if (user == null)
                throw new Exception("User not found");

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                throw new Exception("Invalid email/phone or password");

            var otpCode = _otpService.GenerateOtp();
            user.OtpCode = otpCode;
            user.OtpExpiresAt = DateTime.UtcNow.AddMinutes(10);
            await _userManager.UpdateAsync(user);

            await _otpService.SendOtpAsync(request.EmailOrPhone, otpCode, "email");

            return new OtpResponse
            {
                OtpSent = true,
                Message = "OTP sent successfully",
                ExpiresAt = DateTime.UtcNow.AddMinutes(10)
            };
        }

        public async Task<AuthResponse> VerifyOtpAsync(VerifyOtpRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone)
                    ?? await _userManager.Users
                           .FirstOrDefaultAsync(u => u.PhoneNumber == request.EmailOrPhone
                                                  || u.UserName == request.EmailOrPhone);

            if (user == null)
                throw new Exception("User not found");

            Console.WriteLine($"==========================================");
            Console.WriteLine($"VERIFY OTP DEBUG");
            Console.WriteLine($"User found: {user.UserName}");
            Console.WriteLine($"Stored OTP: {user.OtpCode}");
            Console.WriteLine($"Input OTP:  {request.OtpCode}");
            Console.WriteLine($"Expires At: {user.OtpExpiresAt}");
            Console.WriteLine($"Current UTC: {DateTime.UtcNow}");
            Console.WriteLine($"Is Expired: {DateTime.UtcNow > user.OtpExpiresAt}");
            Console.WriteLine($"==========================================");

            if (string.IsNullOrEmpty(user.OtpCode))
                throw new Exception("No OTP found. Please register again.");

            if (user.OtpExpiresAt == null)
                throw new Exception("OTP expiry not set. Please register again.");

            if (DateTime.UtcNow > user.OtpExpiresAt.Value)
                throw new Exception("OTP has expired. Please request a new one.");

            var isValid = _otpService.ValidateOtp(user.OtpCode, user.OtpExpiresAt, request.OtpCode);

            if (!isValid)
                throw new Exception("Invalid OTP code.");

            user.IsVerified = true;
            user.OtpCode = null;
            user.OtpExpiresAt = null;
            await _userManager.UpdateAsync(user);

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                User = new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.AgencyName ?? string.Empty,
                    UserType = user.UserType,
                    AccountType = user.AccountType,
                    IsVerified = user.IsVerified
                }
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone)
                    ?? await _userManager.Users
                           .FirstOrDefaultAsync(u => u.PhoneNumber == request.EmailOrPhone
                                                  || u.UserName == request.EmailOrPhone);

            if (user == null)
                throw new Exception("Invalid email or password");

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                throw new Exception("Invalid email or password");

            if (!user.IsVerified)
                throw new Exception("Account not verified. Please complete OTP verification.");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                User = new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.AgencyName ?? string.Empty,
                    UserType = user.UserType,
                    AccountType = user.AccountType,
                    IsVerified = user.IsVerified
                }
            };
        }
    }
}