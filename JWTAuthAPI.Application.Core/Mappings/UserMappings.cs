using JWTAuthAPI.Application.Core.DTOs.UserAccount;
using JWTAuthAPI.Application.Core.Entities.Identity;

namespace JWTAuthAPI.Application.Core.Mappings
{
    public static class UserMappings
    {
        public static UserResponse ToResponseDTO(this ApplicationUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UserResponse()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static List<UserResponse> ToResponseDTO(this IReadOnlyCollection<ApplicationUser> users)
        {
            List<UserResponse> usersResponse = new();

            if (users != null)
            {
                usersResponse.AddRange(users.Select(user => user.ToResponseDTO()));
            }

            return usersResponse;
        }

        public static ApplicationUser ToEntity(this UserCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };
        }

        public static ApplicationUser UpdateEntity(this ApplicationUser user,
            UserUpdateRequest request)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (request != null)
            {
                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;
            }

            return user;
        }
    }
}
