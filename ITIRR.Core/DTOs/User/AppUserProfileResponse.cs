namespace ITIRR.Core.DTOs.User
{
    public class AppUserProfileResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string Role { get; set; } = "User";

        public string? AgencyName { get; set; }
        public string? About { get; set; }
        public string? Languages { get; set; }
        public string? ProfilePhotoUrl { get; set; }

        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
    }
}