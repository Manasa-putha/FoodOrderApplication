using FOABE.Dtos;
using FOABE.Models;

namespace FOABE.Bussiness_Layer
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync(RestaurantDto restaurantDto);
        Task UpdateRestaurantAsync(int restaurantId, RestaurantDto restaurantDto);
        Task DeleteRestaurantAsync(int restaurantId);
        Task<MenuDto> AddMenuAsync(int restaurantId, MenuDto menuDto);
        Task UpdateMenuAsync(int menuId, MenuDto menuDto);
        Task DeleteMenuAsync(int menuId);
        Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
        Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId);
        Task<MenuDto> GetMenuByIdAsync(int menuId);
    }

}
