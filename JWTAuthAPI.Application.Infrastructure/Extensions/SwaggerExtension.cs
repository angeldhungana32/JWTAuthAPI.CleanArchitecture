using JWTAuthAPI.Application.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JWTAuthAPI.Application.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerConfiguration>(configuration.GetSection("Swagger"));
            var swaggerConfiguration = configuration.GetSection("Swagger").Get<SwaggerConfiguration>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerConfiguration.Version, 
                    new OpenApiInfo 
                    { 
                        Title = swaggerConfiguration.Title, 
                        Version = swaggerConfiguration.Version 
                    });

                options.AddSecurityDefinition(swaggerConfiguration.SecurityDefinitionName, 
                    new OpenApiSecurityScheme
                    {
                        Description = swaggerConfiguration.SecuritySchemeDescription,
                        Name = swaggerConfiguration.SecuritySchemeName,
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = swaggerConfiguration.SecurityReferenceId,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                };
                options.AddSecurityRequirement(security);
            });

            return services;
        }
    }
}
