using System.ComponentModel.DataAnnotations;

namespace ITIRR.Services.DTOs.Auth
{
    public class RegisterPartnerRequest
    {
        [Required]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string AgencyName { get; set; } = string.Empty;

        public string ContactType { get; set; } = string.Empty;
    }
}