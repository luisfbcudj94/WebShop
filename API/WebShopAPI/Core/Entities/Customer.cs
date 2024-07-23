using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Core.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; }
    }
}
