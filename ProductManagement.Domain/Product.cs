using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "The Name field is required.")]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "The Brand field is required.")]
        public required string Brand { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "The Price field must be greater than 0.")]
        public decimal Price { get; set; }
    }
}