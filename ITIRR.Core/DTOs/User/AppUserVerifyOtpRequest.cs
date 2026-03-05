namespace ITIRR.Core.DTOs.User
{
    public class AppUserVerifyOtpRequest
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string OtpCode { get; set; } = string.Empty;
    }
}