using JWTAuthAPI.Application.Core.AuthorizationRequirement;
using JWTAuthAPI.Application.Core.DTOs.Authentication;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Helpers;
using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Core.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JWTAuthAPI.Application.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IRepositoryActivator _repositoryActivator;

        public AccountService(UserManager<ApplicationUser> userManager, ITokenService tokenService,
            IAuthorizationService authorizationService, 
            IRepositoryActivator repositoryActivator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authorizationService = authorizationService;
            _repositoryActivator = repositoryActivator;
        }

        public async Task<ApplicationUser?> AddUserAsync(ApplicationUser entity, string password)
        {
            await _userManager.CreateAsync(entity, password);
            var validUser = await _userManager.FindByEmailAsync(entity.Email);
            if (validUser != null)
            {
                await _userManager.AddToRoleAsync(validUser, Roles.USER.ToString());
            }
            return validUser;
        }

        public async Task<AuthenticateResponse?> AuthenticateUserAsync(AuthenticateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var isValidUser = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isValidUser)
                {
                    string token = _tokenService.GenerateAuthenticationToken(user);
                    return user.ToResponseDTO(token);
                }
            }

            return default;
        }

        public async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> AuthorizeOwnerAsync(ClaimsPrincipal userContext, ApplicationUser resource)
        {
            var authorized = await _authorizationService.AuthorizeAsync(userContext, resource,
                    new UserIsOwnerRequirement());

            return authorized.Succeeded;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsers()
        {
            return await _repositoryActivator.Repository<ApplicationUser>().ListAllAsync();
        }
    }
}
