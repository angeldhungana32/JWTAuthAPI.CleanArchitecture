using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application.Core.AuthorizationRequirement
{
    public class UserIsOwnerRequirement : IAuthorizationRequirement { }
}
