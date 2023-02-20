using FluentValidation;
using JWTAuthAPI.Application.Core.DTOs.UserAccount;

namespace JWTAuthAPI.Application.API.Validations
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator() 
        {
            RuleFor(v => v.Password)
               .NotNull()
               .NotEmpty()
               .MinimumLength(8).WithMessage("Your password length must be at least 8.")
               .MaximumLength(20).WithMessage("Your password length must not exceed 20.")
               .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
               .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
               .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
               .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(300);

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
