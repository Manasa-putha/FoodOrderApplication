using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FOABE.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        // Foreign key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // Foreign key for Restaurant
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        // Foreign key for Menu/Dish
        [ForeignKey("Menu")]
        public int DishId { get; set; }

        public Menu Dish { get; set; }

        //[Range(1, 5)]
        public int Rating { get; set; }

        public string? ReviewText { get; set; }

    }
}
