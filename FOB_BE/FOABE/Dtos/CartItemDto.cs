namespace FOABE.Dtos
{
    public class CartItemDto
    {
        public int? MenuId { get; set; }
        public string? DishName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
