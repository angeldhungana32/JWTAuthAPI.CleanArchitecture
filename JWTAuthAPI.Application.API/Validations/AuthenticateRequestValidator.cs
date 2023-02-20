using FluentValidation;
using JWTAuthAPI.Application.Core.DTOs.Authentication;

namespace JWTAuthAPI.Application.API.Validations
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(300);

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
