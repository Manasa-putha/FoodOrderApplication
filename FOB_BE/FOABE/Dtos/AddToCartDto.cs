using System.ComponentModel.DataAnnotations;

namespace FOABE.Dtos
{

    public class AddToCartDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public CartItemDto Item { get; set; }



    }
}
