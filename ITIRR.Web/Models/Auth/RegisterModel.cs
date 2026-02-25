using System.ComponentModel.DataAnnotations;

namespace ITIRR.Web.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Agency name is required")]
        public string AgencyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email or phone is required")]
        public string EmailOrPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string ContactType { get; set; } = "email";
    }
}