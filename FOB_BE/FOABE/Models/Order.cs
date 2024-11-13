using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FOABE.Models
{


    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Foreign key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // Foreign key for Restaurant
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        // [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public ICollection<Menu>? MenuItems { get; set; }
        public ICollection<CartItem>? Items { get; set; }
    }
}
