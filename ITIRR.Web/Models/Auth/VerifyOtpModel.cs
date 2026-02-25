using System.ComponentModel.DataAnnotations;

namespace ITIRR.Web.Models.Auth
{
    public class VerifyOtpModel
    {
        [Required]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP is required")]
        public string OtpCode { get; set; } = string.Empty;
    }
}