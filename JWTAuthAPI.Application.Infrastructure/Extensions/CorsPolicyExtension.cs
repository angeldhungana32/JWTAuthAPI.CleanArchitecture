using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthAPI.Application.Infrastructure.Extensions
{
    public static class CorsPolicyExtension
    {
        private static readonly string MyAllowSpecificOrigins = "MyCorsPolicy";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            return services;
        }

        public static IApplicationBuilder UseMyCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(MyAllowSpecificOrigins);
            return app;
        }
    }
}
