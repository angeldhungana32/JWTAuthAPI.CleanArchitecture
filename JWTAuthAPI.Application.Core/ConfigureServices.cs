using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthAPI.Application.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
