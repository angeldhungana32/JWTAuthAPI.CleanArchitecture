namespace JWTAuthAPI.Application.Core.DTOs.Product
{
    public class ProductUpdateRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
    }
}
