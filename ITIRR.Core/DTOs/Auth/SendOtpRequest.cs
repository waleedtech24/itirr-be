using System.ComponentModel.DataAnnotations;

namespace ITIRR.Services.DTOs.Auth
{
    public class SendOtpRequest
    {
        [Required]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}