using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FOABE.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        // Foreign key for Restaurant
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        //[Required]
        public string? DishName { get; set; }

        // [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; } = string.Empty;
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
