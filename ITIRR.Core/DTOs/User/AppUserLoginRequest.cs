namespace ITIRR.Core.DTOs.User
{
    public class AppUserLoginRequest
    {
        public string EmailOrPhone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}