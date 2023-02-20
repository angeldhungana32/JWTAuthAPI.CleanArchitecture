namespace JWTAuthAPI.Application.Core.Configurations
{
    public class JwtConfiguration
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public string SecretKey { get; set; } = "This is a Secret Key";
        public bool RequireHttps { get; set; } = true;
        public bool SaveToken { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true; 
        public bool ValidateIssuer { get; set; } = true; 
        public bool ValidateAudience { get; set; } = true;
        public int Expiration { get; set; } = 1;
        public bool ValidateLifeTime { get; set; } = true;
    }
}