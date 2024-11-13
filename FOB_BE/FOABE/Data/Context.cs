using FOABE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace FOABE.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
       public DbSet<CartItem> Cartitems { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(res => res.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Dish)
                .WithMany()
                .HasForeignKey(r => r.DishId)
                .OnDelete(DeleteBehavior.Cascade);
      //      modelBuilder.Entity<CartItem>()
      //.HasOne(ci => ci.Menu)
      //.WithMany(m => m.CartItems)
      //.HasForeignKey(ci => ci.MenuId)
      //.OnDelete(DeleteBehavior.SetNull); 
          modelBuilder.Entity<CartItem>()
        .HasKey(ci => ci.CartItemId);

       
            modelBuilder.Entity<CartItem>()
     .HasOne(ci => ci.Cart)
     .WithMany(c => c.Items)
     .HasForeignKey(ci => ci.CartId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                userName = "Admin",
                Email = "admin@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Admin,
                Password = "admin1234",
                CreatedAt = new DateTime(2024, 06, 11, 13, 28, 12),
                UpdatedAt = new DateTime(2024, 07, 03, 09, 20, 12),
                PinCode = "12345",
                City = "Kadapa",
                Address = "AP",
                AlternativeNumber = "12345",


            },
            new User()
            {
                Id = 2,
                userName = "sai",
                Email = "sai@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Customer,
                Password = "sai1234",
                CreatedAt = new DateTime(2024, 03, 06, 13, 30, 12),
                UpdatedAt = new DateTime(2024, 06, 12, 09, 20, 12),
                PinCode = "54321",
                City = "Kurnool",
                Address = "AP",
                AlternativeNumber = "12345",

            },
            new User()
            {
                Id = 3,
                userName = "manu",
                Email = "manu@gmail.com",
                MobileNumber = "1234567890",
                UserType = UserType.Customer,
                Password = "manu4321",
                CreatedAt = new DateTime(2024, 01, 06, 14, 30, 12),
                UpdatedAt = new DateTime(2024, 09, 03, 09, 20, 12),
                PinCode = "33333",
                City = "Guntur",
                Address = "AP",
                AlternativeNumber = "12345",


            },
              new User()
              {
                  Id = 4,
                  userName = "Rani",
                  Email = "rani@gmail.com",
                  MobileNumber = "1234567890",
                  UserType = UserType.Customer,
                  Password = "rani43",
                  CreatedAt = new DateTime(2024, 03, 06, 20, 40, 12),
                  UpdatedAt = new DateTime(2024, 06, 03, 09, 20, 12),
                  PinCode = "55555",
                  City = "Anantpur",
                  Address = "AP",
                  AlternativeNumber = "12345",

              }
        );
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "John's Diner",
                    Location = "456 Diner Street, Foodville",
                    Cuisine = "American",
                    OwnerId = 3
                },
                new Restaurant
                {
                    RestaurantId = 2,
                    RestaurantName = "Jane's Italian Eatery",
                    Location = "789 Pasta Lane, Foodtown",
                    Cuisine = "Italian",
                    OwnerId = 3
                }

            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu
                {
                    MenuId = 1,
                    RestaurantId = 1, 
                    DishName = "Cheeseburger",
                    Price = 8.99m,
                    Description = "Juicy cheeseburger with fresh toppings."
                },
                new Menu
                {
                    MenuId = 2,
                    RestaurantId = 1, 
                    DishName = "Fries",
                    Price = 3.50m,
                    Description = "Crispy golden fries."
                },
                new Menu
                {
                    MenuId = 3,
                    RestaurantId = 2,
                    DishName = "Spaghetti Bolognese",
                    Price = 12.50m,
                    Description = "Classic spaghetti with rich bolognese sauce."
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    UserId = 2,
                    RestaurantId = 1, 
                    OrderDate = DateTime.Now,
                    OrderStatus = OrderStatus.Placed
                },
                new Order
                {
                    OrderId = 2,
                    UserId = 2,
                    RestaurantId = 2, 
                    OrderDate = DateTime.Now,
                    OrderStatus = OrderStatus.Preparing
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    UserId = 2,
                    RestaurantId = 1, 
                    DishId = 1,
                    Rating = 5,
                    ReviewText = "Amazing cheeseburger!",
                   
                },
                new Review
                {
                    ReviewId = 2,
                    UserId = 2, 
                    RestaurantId = 2,
                    DishId = 3,
                    Rating = 4,
                    ReviewText = "Delicious pasta, but could use a bit more sauce."
                }
            );
        }




        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<UserType>().HaveConversion<string>();
            configurationBuilder.Properties<OrderStatus>().HaveConversion<string>();
            // configurationBuilder.Properties<AccountStatus>().HaveConversion<string>();
        }

    }
}