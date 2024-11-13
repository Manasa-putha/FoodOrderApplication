using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FOABE.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        [ForeignKey("Menu")]
        public int ?MenuId { get; set; }
        public Menu? Menu { get; set; }

        // [Required]
        public int? Quantity { get; set; }
    }



}
