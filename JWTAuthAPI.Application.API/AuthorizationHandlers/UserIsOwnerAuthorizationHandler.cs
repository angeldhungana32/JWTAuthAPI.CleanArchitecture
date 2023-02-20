using JWTAuthAPI.Application.Core.AuthorizationRequirement;
using JWTAuthAPI.Application.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthAPI.Application.API.AuthorizationHandlers
{
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<UserIsOwnerRequirement, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserIsOwnerAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                   UserIsOwnerRequirement requirement,
                                   ApplicationUser resource)
        {
            if (context.User == null || resource == null) { return Task.CompletedTask; }

            if (resource.Id.ToString() == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
