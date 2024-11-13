using FOABE.Dtos;
using FOABE.Models;

namespace FOABE.Repositories
{
    public interface IRestaurantRepository
    {
        Task AddAsync(Restaurant restaurant);
        Task<Restaurant> GetByIdAsync(int id);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(int id);
        Task AddMenuAsync(Menu menu);
        Task<Menu> GetMenuByIdAsync(int id);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(int id);
        Task<Order> GetOrderByIdAsync(int id);
        Task UpdateOrderAsync(Order order);
        Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId);
    }
}
