using FOABE.Models;

namespace FOABE.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public ICollection<MenuDto>? MenuItems { get; set; }

        public List<OrderItemDto>? Items { get; set; }
    }
}
