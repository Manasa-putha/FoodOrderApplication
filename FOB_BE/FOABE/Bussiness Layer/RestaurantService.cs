using FOABE.Bussiness_Layer;
using FOABE.Dtos;
using FOABE.Models;
using FOABE.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOABE.Services
{
   
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task AddRestaurantAsync(RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                RestaurantName = restaurantDto.RestaurantName,
                Location = restaurantDto.Location,
                Cuisine = restaurantDto.Cuisine,
                OwnerId = restaurantDto.OwnerId
            };
            await _restaurantRepository.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(int restaurantId, RestaurantDto restaurantDto)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found.");

            restaurant.RestaurantName = restaurantDto.RestaurantName;
            restaurant.Location = restaurantDto.Location;
            restaurant.Cuisine = restaurantDto.Cuisine;

            await _restaurantRepository.UpdateAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            await _restaurantRepository.DeleteAsync(restaurantId);
        }

        public async Task<MenuDto> AddMenuAsync(int restaurantId, MenuDto menuDto)
        {
            var menuItem = new Menu
            {
                RestaurantId = restaurantId,
                DishName = menuDto.DishName,
                Price = menuDto.Price,
                Description = menuDto.Description
            };

            await _restaurantRepository.AddMenuAsync(menuItem);
            return menuDto; // Return the created MenuDto or updated version
        }

        public async Task UpdateMenuAsync(int menuId, MenuDto menuDto)
        {
            var menuItem = await _restaurantRepository.GetMenuByIdAsync(menuId);
            if (menuItem == null) throw new KeyNotFoundException("Menu item not found.");

            menuItem.DishName = menuDto.DishName ?? menuItem.DishName;
            menuItem.Price = menuDto.Price;
            menuItem.Description = menuDto.Description;

            await _restaurantRepository.UpdateMenuAsync(menuItem);
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            await _restaurantRepository.DeleteMenuAsync(menuId);
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _restaurantRepository.GetOrderByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Order not found.");

            order.OrderStatus = newStatus;
            await _restaurantRepository.UpdateOrderAsync(order);
            return order;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId)
        {
            var reviews = await _restaurantRepository.GetReviewsByRestaurantIdAsync(restaurantId);
            return reviews;
        }

        public async Task<MenuDto> GetMenuByIdAsync(int menuId)
        {
            var menuItem = await _restaurantRepository.GetMenuByIdAsync(menuId);
            if (menuItem == null) throw new KeyNotFoundException("Menu item not found.");

            return new MenuDto
            {
                MenuId = menuItem.MenuId,
                DishName = menuItem.DishName,
                Price = menuItem.Price,
                Description = menuItem.Description
            };
        }
    }
}
