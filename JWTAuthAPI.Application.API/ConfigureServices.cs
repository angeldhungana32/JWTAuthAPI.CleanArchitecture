using FluentValidation;
using JWTAuthAPI.Application.API.AuthorizationHandlers;
using JWTAuthAPI.Application.API.Validations;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthAPI.Application.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();
            services.AddValidatorsFromAssemblyContaining<AuthenticateRequestValidator>();

            return services;
        }
    }
}
