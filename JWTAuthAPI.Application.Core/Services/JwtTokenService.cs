using JWTAuthAPI.Application.Core.Configurations;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthAPI.Application.Core.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtTokenService(IOptions<JwtConfiguration> configuration)
        {
            _jwtConfiguration = configuration.Value;
        }

        public string GenerateAuthenticationToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _jwtConfiguration.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss, _jwtConfiguration.Issuer)
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtConfiguration.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfiguration.Audience,
                Issuer = _jwtConfiguration.Issuer
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
