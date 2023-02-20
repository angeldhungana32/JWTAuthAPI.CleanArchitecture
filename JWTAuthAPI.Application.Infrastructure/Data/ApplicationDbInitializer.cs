using JWTAuthAPI.Application.Core.Configurations;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JWTAuthAPI.Application.Infrastructure.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AdminConfiguration _adminConfiguration;
        private readonly ILogger<ApplicationDbInitializer> _logger;
        private readonly IConfiguration _configuration;

        public ApplicationDbInitializer(UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           IOptions<AdminConfiguration> adminConfiguration,
           ApplicationDbContext context,
           IConfiguration configuration,
           ILogger<ApplicationDbInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _adminConfiguration = adminConfiguration.Value;
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        public async Task InitializeDBAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating database");
                throw;
            }
        }

        public async Task SeedDBAsync()
        {
            try
            {
                await TrySeedRoles();
                await TrySeedAdmin();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while seeding database");
            }
        }

        private async Task TrySeedRoles()
        {
            RoleConfiguration roleConfiguration = new();
            _configuration.GetSection(ConfigurationSectionKeyConstants.Roles).Bind(roleConfiguration);
            
            if (roleConfiguration.AvailableRoles != null)
            {
                foreach(string role in roleConfiguration.AvailableRoles)
                {
                    var result = await _roleManager.RoleExistsAsync(role);
                    if (!result)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole()
                        {
                            Name = role
                        });
                    }
                }
            }           
        }

        private async Task TrySeedAdmin()
        {
            if (await _userManager.FindByEmailAsync(_adminConfiguration.Email) == null)
            {
                ApplicationUser user = new()
                {
                    Email = _adminConfiguration.Email,
                    UserName = _adminConfiguration.Email,
                    FirstName = _adminConfiguration.FirstName,
                    LastName = _adminConfiguration.LastName
                };

                var result = await _userManager.CreateAsync(user, _adminConfiguration.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,Roles.ADMIN).Wait();
                }
            }
        }
    }
}
