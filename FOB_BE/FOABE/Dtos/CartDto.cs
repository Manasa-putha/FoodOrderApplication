namespace FOABE.Dtos
{


    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto>? Items { get; set; }
    }


}
