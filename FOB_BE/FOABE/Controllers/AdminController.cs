using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FOABE.Data;
using FOABE.Models;
using FOABE.Dtos;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace FOABE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AdminController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("registerRestaurant")]
        public async Task<IActionResult> RegisterRestaurant(RestaurantDto restaurantDto)
        {
            try
            {
                var owner = await _context.Users.FindAsync(restaurantDto.OwnerId);
            if (owner == null || owner.UserType != UserType.Admin)
                return BadRequest("Invalid owner ID or the user is not a RestaurantOwner.");

            var restaurant = new Restaurant
            {
                RestaurantName = restaurantDto.RestaurantName,
                Location = restaurantDto.Location,
                Cuisine = restaurantDto.Cuisine,
                OwnerId = restaurantDto.OwnerId
            };

            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Restaurant registered successfully!" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while registering the restaurant.");
            }
        }

        [HttpPost("{restaurantId}/menu")]
        public async Task<IActionResult> AddMenuItem(int restaurantId, MenuDto menuDto)
        {
            try
            {
                var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null) return NotFound("Restaurant not found.");

            var menuItem = new Menu
            {
                RestaurantId = restaurantId,
                DishName = menuDto.DishName,
                Price = menuDto.Price,
                Description = menuDto.Description
            };

            await _context.Menus.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            var addedMenuItemDto = _mapper.Map<MenuDto>(menuItem);

            return Ok(new { message = "Menu item added successfully!", updatedItem = addedMenuItemDto });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while adding the menu item.");
            }
        }


        [HttpPut("{restaurantId}/editMenuItem/{menuId}")]
        public async Task<IActionResult> EditMenuItem(int menuId, MenuDto menuDto)
        {
            try
            {
                var menuItem = await _context.Menus.FindAsync(menuId);
            if (menuItem == null) return NotFound("Menu item not found.");

            menuItem.DishName = menuDto.DishName ?? menuItem.DishName;
            menuItem.Price = menuDto.Price;
            menuItem.Description = menuDto.Description;

            _context.Menus.Update(menuItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Menu item updated successfully!", updatedItem = menuItem });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while editing the menu item.");
            }
        }


        [HttpDelete("{restaurantId}/deleteMenuItem/{menuId}")]
        public async Task<IActionResult> DeleteMenuItem(int menuId)
        {

            try
            {
                var menuItem = await _context.Menus.FindAsync(menuId);
            if (menuItem == null) return NotFound("Menu item not found.");

            _context.Menus.Remove(menuItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Menu item deleted successfully!" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while deleting the menu item.");
            }
        }

        [HttpPut("processOrder/{orderId}")]
        public async Task<IActionResult> ProcessOrder(int orderId, [FromBody] OrderStatus newStatus)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound("Order not found.");

            order.OrderStatus = newStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Order status updated to {newStatus}." });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while processing the order.");
            }
        }

        [HttpGet("restaurantReviews/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantReviews(int restaurantId)
        {
            try
            {
                var restaurant = await _context.Restaurants
                .Include(r => r.Reviews)
                .ThenInclude(review => review.User)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (restaurant == null) return NotFound("Restaurant not found.");

            var reviews = restaurant.Reviews.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                RestaurantId = r.RestaurantId,
                DishId = r.DishId,
                Rating = r.Rating,
                ReviewText = r.ReviewText
            }).ToList();

            return Ok(reviews);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving reviews.");
            }
        }

        [HttpPost("respondReview/{reviewId}")]
        public async Task<IActionResult> RespondToReview(int reviewId, [FromBody] string response)
        {
            try
            {
                var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return NotFound("Review not found.");

            return Ok(new { message = "Response sent successfully!" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving reviews.");
            }
        }

        [HttpDelete("deleteRestaurant/{restaurantId}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            try
            {
                var restaurant = await _context.Restaurants
                                           .Include(r => r.Menus)
                                           .Include(r => r.Orders)
                                           .Include(r => r.Reviews)
                                           .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }
            var menuIds = restaurant.Menus.Select(m => m.MenuId).ToList();
            var cartItems = _context.Cartitems.Where(ci => menuIds.Contains((int)ci.MenuId)).ToList();
            _context.Cartitems.RemoveRange(cartItems);
            _context.Menus.RemoveRange(restaurant.Menus);
            _context.Orders.RemoveRange(restaurant.Orders);
            _context.Reviews.RemoveRange(restaurant.Reviews);

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while delete ");
    }
}
        [HttpPut("updateRestaurant/{restaurantId}")]

        public async Task<IActionResult> UpdateRestaurant(int restaurantId, RestaurantDto restaurantDto)
        {

            var restaurant = await _context.Restaurants
                .Include(r => r.Menus)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (restaurant == null)
            {
                return NotFound();
            }
            restaurant.RestaurantName = restaurantDto.RestaurantName;
            restaurant.Location = restaurantDto.Location;
            restaurant.Cuisine = restaurantDto.Cuisine;
            foreach (var menuDto in restaurantDto.Menus)
            {
                var existingMenu = restaurant.Menus.FirstOrDefault(m => m.MenuId == menuDto.MenuId);

                if (existingMenu != null)
                {
                    existingMenu.DishName = menuDto.DishName;
                    existingMenu.Price = menuDto.Price;
                    existingMenu.Description = menuDto.Description;
                }
                else
                {
                    restaurant.Menus.Add(new Menu
                    {
                        DishName = menuDto.DishName,
                        Price = menuDto.Price,
                        Description = menuDto.Description,
                        RestaurantId = restaurantId
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return NoContent();
        }

        [HttpGet("menu/{id}")]
        public async Task<ActionResult<Menu>> GetMenuById(int id)
        {
            try
            {

                var menu = await _context.Menus.FindAsync(id);

                if (menu == null)
                {
                    return NotFound();
                }

                return Ok(menu); // Return the menu details
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving .");
            }
        }

    }


}
