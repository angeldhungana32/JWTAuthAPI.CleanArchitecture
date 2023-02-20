using JWTAuthAPI.Application.Core.DTOs.Authentication;
using JWTAuthAPI.Application.Core.Entities.Identity;
using System.Security.Claims;

namespace JWTAuthAPI.Application.Core.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password);
        Task<bool> UpdateUserAsync(ApplicationUser entity);
        Task<bool> DeleteUserAsync(ApplicationUser entity);
        Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request);
        Task<bool> AuthorizeOwnerAsync(ClaimsPrincipal userContext, ApplicationUser resource);
    }
}
