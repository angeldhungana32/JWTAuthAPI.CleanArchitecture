using JWTAuthAPI.Application.Core.Entities;

namespace JWTAuthAPI.Application.Core.Specifications
{
    public class ProductsByUserId : BaseSpecification<Product>
    {
        public ProductsByUserId(Guid userId) : base(x => x.UserId == userId) { }
    }
}
