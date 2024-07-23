using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Core.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        [StringLength(int.MaxValue)]
        public string ImageBase64 { get; set; }

        // Navigation properties
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
