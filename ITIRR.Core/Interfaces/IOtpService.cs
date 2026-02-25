namespace ITIRR.Core.Interfaces
{
    public interface IOtpService
    {
        string GenerateOtp();
        Task<bool> SendOtpAsync(string emailOrPhone, string otpCode, string contactType);
        bool ValidateOtp(string storedOtp, DateTime? expiresAt, string inputOtp);
    }
}