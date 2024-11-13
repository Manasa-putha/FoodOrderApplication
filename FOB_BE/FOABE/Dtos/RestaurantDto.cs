namespace FOABE.Dtos
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Cuisine { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public ICollection<MenuDto>? Menus { get; set; }
        public ICollection<OrderDto>? Orders { get; set; }
        public ICollection<ReviewDto>? Reviews { get; set; }
    }

}
