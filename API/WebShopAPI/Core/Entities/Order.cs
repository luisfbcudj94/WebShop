using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Core.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
