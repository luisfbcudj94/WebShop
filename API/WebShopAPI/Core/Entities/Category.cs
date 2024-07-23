using System.ComponentModel.DataAnnotations;

namespace WebShopAPI.Core.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        // Navigation properties
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
