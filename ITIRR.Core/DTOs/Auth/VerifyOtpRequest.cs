using System.ComponentModel.DataAnnotations;

namespace ITIRR.Services.DTOs.Auth
{
    public class VerifyOtpRequest
    {
        [Required(ErrorMessage = "Email or phone is required")]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 digits")]
        public string OtpCode { get; set; } = string.Empty;

        public string ContactType { get; set; } = "Email"; 
    }
}