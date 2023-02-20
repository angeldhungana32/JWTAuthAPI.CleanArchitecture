using JWTAuthAPI.Application.API.Extensions;
using JWTAuthAPI.Application.Core.Configurations;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Infrastructure.Data;
using JWTAuthAPI.Application.Infrastructure.Extensions;
using JWTAuthAPI.Application.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthAPI.Application.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseInMemoryDatabase("JWTAuthAPIDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddIdentityCore<ApplicationUser>(
                options =>
                {
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.User.RequireUniqueEmail = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<AdminConfiguration>(configuration.GetSection("AdminCredentials"));
            services.Configure<RoleConfiguration>(configuration.GetSection("AvailableRoles"));

            services.AddTransient<ApplicationDbInitializer>();
            services.AddTransient<IRepositoryActivator, RepositoryActivator>();

            services.AddSwaggerCustom(configuration);
            services.AddJwtAuthentication(configuration);

            return services;
        }
    }
}
