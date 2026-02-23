namespace ITIRR.Services.DTOs.Auth
{
    public class OtpResponse
    {
        public bool OtpSent { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}