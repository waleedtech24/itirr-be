using ITIRR.Core.DTOs.User;

namespace ITIRR.Core.Interfaces
{
    public interface IAppUserAuthService
    {
        Task<AppUserAuthResponse> RegisterAsync(AppUserRegisterRequest request);
        Task<AppUserAuthResponse> VerifyOtpAsync(AppUserVerifyOtpRequest request);
        Task<AppUserAuthResponse> LoginAsync(AppUserLoginRequest request);
        Task<AppUserAuthResponse> ResendOtpAsync(string emailOrPhone);
    }
}