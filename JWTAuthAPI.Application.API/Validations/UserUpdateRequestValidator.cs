using FluentValidation;
using JWTAuthAPI.Application.Core.DTOs.UserAccount;

namespace JWTAuthAPI.Application.API.Validations
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator() 
        {
            RuleFor(v => v.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(v => v.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
