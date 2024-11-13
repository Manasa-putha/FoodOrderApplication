using FOABE.Data;
using FOABE.Dtos;
using FOABE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOABE.Repositories
{
   

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly Context _context;

        public RestaurantRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await GetByIdAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddMenuAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(int id)
        {
            var menu = await GetMenuByIdAsync(id);
            if (menu == null)
            {
                throw new KeyNotFoundException($"Menu item with ID {id} not found.");
            }

            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Reviews
                .Where(r => r.RestaurantId == restaurantId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    //Comment = r.Comment
                })
                .ToListAsync();
        }
    }
}
