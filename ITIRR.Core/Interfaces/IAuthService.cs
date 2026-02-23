using ITIRR.Core.DTOs.Auth;
using ITIRR.Services.DTOs.Auth;

namespace ITIRR.Core.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterPartnerAsync(RegisterPartnerRequest request);
        Task<OtpResponse> SendOtpAsync(SendOtpRequest request);
        Task<AuthResponse> VerifyOtpAsync(VerifyOtpRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);         

    }
}
