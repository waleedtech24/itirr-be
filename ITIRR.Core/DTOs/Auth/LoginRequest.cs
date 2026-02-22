using System.ComponentModel.DataAnnotations;

namespace ITIRR.Core.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        public string EmailOrPhone { get; set; } = null!;
        public string Password { get; set; }=null!;
    }
}