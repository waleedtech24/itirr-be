using ITIRR.Core.DTOs.User;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
namespace ITIRR.Services.Services
{
    public class AppUserAuthService : IAppUserAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AppUserAuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ── REGISTER ──
        public async Task<AppUserAuthResponse> RegisterAsync(AppUserRegisterRequest request)
        {
            // Validate
            if (string.IsNullOrEmpty(request.Email) &&
                string.IsNullOrEmpty(request.PhoneNumber))
                return Fail("Email or phone number is required.");

            if (request.Password != request.ConfirmPassword)
                return Fail("Passwords do not match.");

            if (request.Password.Length < 6)
                return Fail("Password must be at least 6 characters.");

            // Check duplicate
            var exists = await _context.CustomerUsers.AnyAsync(u =>
                (!string.IsNullOrEmpty(request.Email) && u.Email == request.Email) ||
                (!string.IsNullOrEmpty(request.PhoneNumber) && u.PhoneNumber == request.PhoneNumber));

            if (exists)
                return Fail("An account with this email or phone already exists.");

            // Generate OTP
            var otp = GenerateOtp();
            var expiry = DateTime.UtcNow.AddMinutes(10);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsVerified = false,
                OtpCode = otp,
                OtpExpiry = expiry,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.CustomerUsers.Add(user);
            await _context.SaveChangesAsync();

            // TODO: Send OTP via email/SMS service
            Console.WriteLine($"[OTP] {request.Email ?? request.PhoneNumber}: {otp}");

            return new AppUserAuthResponse
            {
                Success = true,
                Message = $"OTP sent to {request.Email ?? request.PhoneNumber}. Please verify."
            };
        }

        // ── VERIFY OTP ──
        public async Task<AppUserAuthResponse> VerifyOtpAsync(AppUserVerifyOtpRequest request)
        {
            var user = await FindUserAsync(request.Email, request.PhoneNumber);

            if (user == null)
                return Fail("User not found.");

            if (user.IsVerified)
                return Fail("Account already verified. Please login.");

            if (string.IsNullOrEmpty(user.OtpCode))
                return Fail("No OTP found. Please register again.");

            if (user.OtpExpiry == null || DateTime.UtcNow > user.OtpExpiry)
                return Fail("OTP has expired. Please request a new one.");

            if (user.OtpCode != request.OtpCode)
                return Fail("Invalid OTP code.");

            // Mark verified
            user.IsVerified = true;
            user.OtpCode = null;
            user.OtpExpiry = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var (accessToken, refreshToken) = GenerateTokens(user);

            return new AppUserAuthResponse
            {
                Success = true,
                Message = "Account verified successfully! Welcome to ITIRR.",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = MapToInfo(user)
            };
        }

        // ── LOGIN ──
        public async Task<AppUserAuthResponse> LoginAsync(AppUserLoginRequest request)
        {
            var user = await _context.CustomerUsers.FirstOrDefaultAsync(u =>
                u.Email == request.EmailOrPhone ||
                u.PhoneNumber == request.EmailOrPhone);

            if (user == null)
                return Fail("Invalid email/phone or password.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Fail("Invalid email/phone or password.");

            if (!user.IsVerified)
                return Fail("Account not verified. Please verify your OTP first.");

            var (accessToken, refreshToken) = GenerateTokens(user);

            return new AppUserAuthResponse
            {
                Success = true,
                Message = "Login successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                User = MapToInfo(user)
            };
        }

        // ── RESEND OTP ──
        public async Task<AppUserAuthResponse> ResendOtpAsync(string emailOrPhone)
        {
            var user = await _context.CustomerUsers.FirstOrDefaultAsync(u =>
                u.Email == emailOrPhone ||
                u.PhoneNumber == emailOrPhone);

            if (user == null)
                return Fail("User not found.");

            if (user.IsVerified)
                return Fail("Account already verified. Please login.");

            var otp = GenerateOtp();
            user.OtpCode = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // TODO: Send OTP via email/SMS service
            Console.WriteLine($"[RESEND OTP] {emailOrPhone}: {otp}");

            return new AppUserAuthResponse
            {
                Success = true,
                Message = "OTP resent successfully."
            };
        }

        // ── HELPERS ──
        private async Task<User?> FindUserAsync(string? email, string? phone) =>
            await _context.CustomerUsers.FirstOrDefaultAsync(u =>
                (!string.IsNullOrEmpty(email) && u.Email == email) ||
                (!string.IsNullOrEmpty(phone) && u.PhoneNumber == phone));

        private static string GenerateOtp() =>
            new Random().Next(100000, 999999).ToString();

        private (string access, string refresh) GenerateTokens(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(24);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,
                    $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email,
                    user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role,           user.Role),
                new Claim("FirstName",               user.FirstName),
                new Claim("LastName",                user.LastName),
                new Claim("UserId",                  user.Id.ToString()),
                new Claim("UserType",                "AppUser")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return (
                new JwtSecurityTokenHandler().WriteToken(token),
                Guid.NewGuid().ToString("N")
            );
        }

        private static AppUserInfo MapToInfo(User u) => new()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            IsVerified = u.IsVerified,
            Role = u.Role
        };

        private static AppUserAuthResponse Fail(string message) => new()
        {
            Success = false,
            Message = message,
            Errors = new List<string> { message }
        };
    }
}