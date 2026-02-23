using ITIRR.Web.Models.Auth;

namespace ITIRR.Web.Services
{
    public interface IAuthApiService
    {
        Task<AuthResult> LoginAsync(LoginModel model);
        Task<AuthResult> RegisterAsync(RegisterModel model);
        Task<AuthResult> VerifyOtpAsync(VerifyOtpModel model);
    }
}