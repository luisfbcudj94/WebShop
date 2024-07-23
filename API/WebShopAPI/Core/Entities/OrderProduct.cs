using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Core.Entities
{
    public class OrderProduct
    {
        [Key]
        public Guid OrderProductId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
