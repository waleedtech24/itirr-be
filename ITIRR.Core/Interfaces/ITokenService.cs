using ITIRR.Core.Entities;

namespace ITIRR.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
    }
}