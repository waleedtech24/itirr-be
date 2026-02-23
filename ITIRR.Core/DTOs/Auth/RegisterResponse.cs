namespace ITIRR.Services.DTOs.Auth
{
    public class RegisterResponse
    {
        public string UserId { get; set; }
        public string EmailOrPhone { get; set; }
        public bool OtpSent { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}