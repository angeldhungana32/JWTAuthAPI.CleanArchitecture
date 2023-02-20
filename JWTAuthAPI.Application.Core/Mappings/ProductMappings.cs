using JWTAuthAPI.Application.Core.DTOs.Product;
using JWTAuthAPI.Application.Core.Helpers;
using System.Linq;

namespace JWTAuthAPI.Application.Core.Mappings
{
    public static class ProductMappings
    {
        public static ProductResponse ToResponseDTO(this Entities.Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            return new ProductResponse()
            {
                Id = product.Id.ToString(),
                Description = product.Description,
                Price = product.Price,
                Name = product.Name,
                Quantity = product.Quantity,
                UserId = product.UserId.ToString()
            };
        }

        public static List<ProductResponse> ToResponseDTO(this IReadOnlyCollection<Entities.Product> products)
        {
            List<ProductResponse> productsResponse = new();

            if (products != null)
            {
                productsResponse.AddRange(products.Select(product => product.ToResponseDTO()));
            }

            return productsResponse;
        }

        public static Entities.Product ToEntity(this ProductCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new Entities.Product()
            {
                Description = request.Description,
                Name = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                UserId = GuidParser.Parse(request.UserId),
            };
        }

        public static Entities.Product UpdateEntity(this Entities.Product product,
            ProductUpdateRequest request)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (request != null)
            {
                product.Price = request.Price ?? product.Price;
                product.Description = request.Description ?? product.Description;
                product.Quantity = request.Quantity ?? product.Quantity;
                product.Name = request.Name ?? product.Name;
            }

            return product;

        }

    }
}
