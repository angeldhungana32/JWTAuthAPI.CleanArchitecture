using FluentValidation;
using JWTAuthAPI.Application.API.AuthorizationHandlers;
using JWTAuthAPI.Application.API.Validations;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace JWTAuthAPI.Application.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions
                    .Converters.Add(new JsonStringEnumConverter()));

            services.AddEndpointsApiExplorer();
            services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();
            services.AddValidatorsFromAssemblyContaining<AuthenticateRequestValidator>();

            return services;
        }
    }
}
