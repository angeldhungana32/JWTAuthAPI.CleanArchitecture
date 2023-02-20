using JWTAuthAPI.Application.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthAPI.Application.Core.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
