using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FOABE.Models
{

    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        //[Required]
        public string RestaurantName { get; set; } = string.Empty;

        //[Required]
        public string Location { get; set; } = string.Empty;

        // [Required]
        public string Cuisine { get; set; } = string.Empty;

        // Foreign key for RestaurantOwner
        [ForeignKey("User")]
        public int OwnerId { get; set; }

        // Navigation property for RestaurantOwner
        public User? User { get; set; }



        // Navigation properties for Menus and Orders
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
