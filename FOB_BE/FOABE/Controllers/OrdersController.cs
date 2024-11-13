using FOABE.Data;
using FOABE.Dtos;
using FOABE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace FOABE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly Context _context;
        private readonly IMapper _mapper;

        public OrdersController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("users")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUserDto = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUserDto.Id }, createdUserDto);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {

            try
            {
                var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [HttpPost("restaurants")]
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantDto restaurantDto)
        {

            try
            {
                var restaurant = _mapper.Map<Restaurant>(restaurantDto);

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            var createdRestaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurantDto.RestaurantId }, createdRestaurantDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while creating the restaurant.");
            }
        }

   
        [HttpGet("restaurants/{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            try
            {
                var restaurant = await _context.Restaurants
                .Include(r => r.User) 
                .Include(r => r.Menus)
                .Include(r => r.Orders)
                .Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the restaurant.");
            }
        }

        //[HttpPost("orders")]
        //public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        //{
        //    var order = _mapper.Map<Order>(orderDto);
        //    order.OrderDate = DateTime.UtcNow;

        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();
        //    var createdOrderDto = _mapper.Map<OrderDto>(order);
        //    return CreatedAtAction(nameof(GetOrder), new { id = createdOrderDto.UserId }, createdOrderDto);
        //}
        //[HttpPost("orders")]
        //public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        //{
        //    if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
        //    {
        //        return BadRequest("Order items must be provided.");
        //    }

        //    var order = new Order
        //    {
        //        UserId = orderDto.UserId,
        //        RestaurantId = orderDto.RestaurantId,
        //        OrderDate = DateTime.UtcNow,
        //        OrderStatus = orderDto.OrderStatus,
        //        //OrderItems = orderDto.Items.Select(item => new OrderItem
        //        //{
        //        //    MenuId = item.MenuId,
        //        //    Quantity = item.Quantity
        //        //    // Include other properties if needed
        //        //}).ToList()
        //    };


        //    var userExists = await _context.Users.FindAsync(order.UserId);
        //    if (userExists == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    var restaurantExists = await _context.Restaurants.FindAsync(order.RestaurantId);
        //    if (restaurantExists == null)
        //    {
        //        return NotFound("Restaurant not found.");
        //    }


        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();


        //    var createdOrderDto = _mapper.Map<OrderDto>(order);

        //    return CreatedAtAction(nameof(GetOrder), new { id = createdOrderDto.UserId }, createdOrderDto);
        //}
        [HttpPost("orders")]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            try
            {
                if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return BadRequest("Order items must be provided.");
            }

            // Validate the user exists
            var userExists = await _context.Users.FindAsync(orderDto.UserId);
            if (userExists == null)
            {
                return NotFound("User not found.");
            }

            // Validate the restaurant exists
            var restaurantExists = await _context.Restaurants.FindAsync(orderDto.RestaurantId);
            if (restaurantExists == null)
            {
                return NotFound("Restaurant not found.");
            }

            // Validate each menu item exists and map the order items
            var orderItems = new List<CartItem>();
            foreach (var itemDto in orderDto.Items)
            {
                var menuItem = await _context.Menus.FindAsync(itemDto.MenuId);
                if (menuItem == null)
                {
                    return NotFound($"Menu item with ID {itemDto.MenuId} not found.");
                }

                var orderItem = new CartItem
                {
                    MenuId = itemDto.MenuId,
                    Quantity = itemDto.Quantity,
                    // You can also calculate the total price if needed
                };
                orderItems.Add(orderItem);

            }

            // Create the order
            var order = new Order
            {
                UserId = orderDto.UserId,
                RestaurantId = orderDto.RestaurantId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = orderDto.OrderStatus,
                Items = orderItems
            };

            // Add the order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Map the created order to OrderDto for the response
            var createdOrderDto = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrder), new { id = createdOrderDto.UserId }, createdOrderDto);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, "An error occurred while creating the order.");
            }

        }


        [HttpGet("orders/{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            try
            {
                var order = await _context.Orders
                .Include(o => o.User) 
                .Include(o => o.Restaurant) 
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the order.");
            }
        }

        [HttpGet("restaurants")]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            try
            {
                var restaurants = await _context.Restaurants
                .Include(r => r.User)
                .Include(r => r.Menus)
                .Include(r => r.Orders)
                .Include(r => r.Reviews)
                .ToListAsync();

            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return Ok(restaurantDtos);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the restaurants.");
            }
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                 .Include(o => o.Items)             // Include the ordered items (CartItems)
            .ThenInclude(i => i.Menu)
                .ToListAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while retrieving the orders.");
            }
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            try
            {
                var reviews = await _context.Reviews.ToListAsync();
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the reviews.");
            }
        }
        [HttpGet("orders/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderDetails(int orderId)
        {
            try
            {
                var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                //.Include(o => o.OrderItems)
                //.ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving the order details.");
            }
        }
        [HttpGet("users/{userId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderHistory(int userId)
        {

            try
            {
                var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Restaurant)
                .ToListAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while retrieving the order history.");
            }
        }
        [HttpGet("orders/{orderId}/status")]
        public async Task<ActionResult<string>> GetOrderStatus(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order.OrderStatus);
        }
            catch (Exception ex)
            {
              
                return StatusCode(500, "An error occurred while retrieving the order status.");
    }
}
        [HttpGet("restaurants/search")]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> SearchRestaurants(string location, string cuisine, string name)
        {
            try
            {
                var restaurants = await _context.Restaurants
                .Where(r =>
                    (string.IsNullOrEmpty(location) || r.Location.Contains(location)) &&
                    (string.IsNullOrEmpty(cuisine) || r.Cuisine.Contains(cuisine)) &&
                    (string.IsNullOrEmpty(name) || r.RestaurantName.Contains(name))
                )
                .Include(r => r.Menus)
                .ToListAsync();

            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return Ok(restaurantDtos);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while retrieving the restaurants");
            }
        }

        [HttpPost("{userId}/add-to-cart")]
        public async Task<ActionResult<CartItemDto>> AddToCart(int userId, CartItemDto cartItemDto)
        {
            try
            {
                var cart = await _context.Carts.Include(c => c.UserId).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                _context.Carts.Add(cart);
            }
            await _context.SaveChangesAsync();
            return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while adding to the cart.");
            }
        }

        [HttpPost("cart/add")]
        public async Task<ActionResult<CartDto>> AddToCart(AddToCartDto addToCartDto)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Cart)
                        .ThenInclude(c => c.Items)
                            .ThenInclude(i => i.Menu)
                    .FirstOrDefaultAsync(u => u.Id == addToCartDto.UserId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                if (user.Cart == null)
                {
                    user.Cart = new Cart { UserId = user.Id };
                }

                var menu = await _context.Menus.FindAsync(addToCartDto.Item.MenuId);
                if (menu == null)
                {
                    return NotFound("Menu item not found");
                }

                var existingItem = user.Cart.Items.FirstOrDefault(i => i.MenuId == addToCartDto.Item.MenuId);
                if (existingItem != null)
                {
                    existingItem.Quantity += addToCartDto.Item.Quantity;
                }
                else
                {
                    user.Cart.Items.Add(new CartItem
                    {
                        MenuId = menu.MenuId,
                        Quantity = addToCartDto.Item.Quantity
                    });
                }

                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<CartDto>(user.Cart));
            
              }
            catch (Exception ex)
            {
              
                return StatusCode(500, "An error occurred while adding to the cart.");
    }
}

        [HttpGet("cart/{userId}")]
        public async Task<ActionResult<CartDto>> GetCart(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.Items)
                        .ThenInclude(i => i.Menu)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Cart == null)
            {
                return NotFound("Cart not found");
            }

            return Ok(_mapper.Map<CartDto>(user.Cart));
        }

        [HttpPut("cart/update")]
        public async Task<ActionResult<CartDto>> UpdateCart(int userId, List<CartItemDto> items)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.Items)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Cart == null)
            {
                return NotFound("Cart not found");
            }

            user.Cart.Items.Clear();
            foreach (var item in items)
            {
                var menu = await _context.Menus.FindAsync(item.MenuId);
                if (menu != null)
                {
                    user.Cart.Items.Add(new CartItem
                    {
                        MenuId = menu.MenuId,
                        Quantity = item.Quantity
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CartDto>(user.Cart));
        }

        [HttpDelete("cart/{userId}/clear")]
        public async Task<ActionResult> ClearCart(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.Items)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Cart == null)
            {
                return NotFound("Cart not found");
            }

            user.Cart.Items.Clear();
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("userOrders/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }


        [HttpPut("updateStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderDto request)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = request.OrderStatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}




