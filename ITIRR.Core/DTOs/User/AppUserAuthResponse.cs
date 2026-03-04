namespace ITIRR.Core.DTOs.User
{
    public class AppUserAuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public AppUserInfo? User { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class AppUserInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string Role { get; set; } = "User";
    }
}