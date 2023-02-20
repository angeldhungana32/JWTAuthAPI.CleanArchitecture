using JWTAuthAPI.Application.Core.Configurations;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly RoleConfiguration _roleConfiguration;
        private readonly ILogger<ApplicationDbInitializer> _logger;

        public ApplicationDbInitializer(UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           IOptions<AdminConfiguration> adminConfiguration,
           IOptions<RoleConfiguration> roleConfiguration,
           ApplicationDbContext context,
           ILogger<ApplicationDbInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _adminConfiguration = adminConfiguration.Value;
            _roleConfiguration = roleConfiguration.Value;
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
            if(_roleConfiguration.Roles != null)
            {
                foreach(string role in _roleConfiguration.Roles)
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
                    _userManager.AddToRoleAsync(user,Roles.Admin.ToString()).Wait();
                }
            }
        }
    }
}
