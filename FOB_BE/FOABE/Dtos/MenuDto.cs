namespace FOABE.Dtos
{

    public class MenuDto
    {
        public int? MenuId { get; set; }
        public int RestaurantId { get; set; }
        public string? DishName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? Quantity { get; internal set; }
    }
}
