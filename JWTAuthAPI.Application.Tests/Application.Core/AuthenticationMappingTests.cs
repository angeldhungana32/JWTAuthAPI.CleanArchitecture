using JWTAuthAPI.Application.Core.DTOs.Authentication;
using JWTAuthAPI.Application.Core.Entities.Identity;
using JWTAuthAPI.Application.Core.Mappings;

namespace JWTAuthAPI.Application.Tests.Application.Core
{
    public class AuthenticationMappingTests
    {
        [Fact]
        public void ShouldMapApplicationUserToAuthenticateResponseDTO()
        {
            // Setup
            ApplicationUser user = new ()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                Id = Guid.NewGuid()
            };

            // Act
            var result = user.ToResponseDTO("token");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.GetType().IsAssignableFrom(typeof(AuthenticateResponse)));
            Assert.NotNull(result.Id);
            Assert.NotNull(result.Email);
            Assert.NotNull(result.FirstName);
            Assert.NotNull(result.LastName);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenMappingNullToResponseDTO()
        {
            ApplicationUser? nullUser = default;
            Assert.Throws<ArgumentNullException>(() => 
                AuthenticationMappings.ToResponseDTO(nullUser, "token"));
        }
    }
}
