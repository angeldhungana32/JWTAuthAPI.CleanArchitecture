using FluentValidation;
using JWTAuthAPI.Application.Core.DTOs.Product;

namespace JWTAuthAPI.Application.API.Validations
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator() 
        {
            RuleFor(v => v.Name)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(100);

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(v => v.UserId)
                .NotNull()
                .NotEmpty();

        }
    }
}
