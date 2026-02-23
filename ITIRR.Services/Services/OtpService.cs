using ITIRR.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ITIRR.Services.Services
{
    public class OtpService : IOtpService
    {
        private readonly IConfiguration _configuration;

        public OtpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateOtp()
        {
            var random = new Random();
            var otpLength = int.Parse(_configuration["OtpSettings:Length"] ?? "6");
            var otp = random.Next(0, (int)Math.Pow(10, otpLength)).ToString($"D{otpLength}");
            return otp;
        }

        public async Task<bool> SendOtpAsync(string emailOrPhone, string otpCode, string contactType)
        {
            // TODO: Integrate actual email/SMS service
            // For now, just log it (in production, use SendGrid/Twilio)

            Console.WriteLine($"==========================================");
            Console.WriteLine($"OTP SENT");
            Console.WriteLine($"To: {emailOrPhone}");
            Console.WriteLine($"Type: {contactType}");
            Console.WriteLine($"Code: {otpCode}");
            Console.WriteLine($"==========================================");

            await Task.CompletedTask;
            return true;
        }

        public bool ValidateOtp(string storedOtp, DateTime? expiresAt, string inputOtp)
        {
            if (string.IsNullOrEmpty(storedOtp) || !expiresAt.HasValue)
                return false;

            if (DateTime.UtcNow > expiresAt.Value)
                return false;

            return storedOtp == inputOtp;
        }
    }
}