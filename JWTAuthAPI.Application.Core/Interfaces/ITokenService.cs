using JWTAuthAPI.Application.Core.Entities.Identity;

namespace JWTAuthAPI.Application.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateAuthenticationToken(ApplicationUser user, List<string> roles);
    }
}
