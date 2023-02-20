using System.ComponentModel.DataAnnotations;

namespace JWTAuthAPI.Application.Core.DTOs.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
