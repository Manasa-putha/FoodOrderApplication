﻿// <auto-generated />
using System;
using FOABE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FOABE.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241014181246_data")]
    partial class data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FOABE.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("FOABE.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("MenuId");

                    b.ToTable("Cartitems");
                });

            modelBuilder.Entity("FOABE.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            Description = "Juicy cheeseburger with fresh toppings.",
                            DishName = "Cheeseburger",
                            Price = 8.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            MenuId = 2,
                            Description = "Crispy golden fries.",
                            DishName = "Fries",
                            Price = 3.50m,
                            RestaurantId = 1
                        },
                        new
                        {
                            MenuId = 3,
                            Description = "Classic spaghetti with rich bolognese sauce.",
                            DishName = "Spaghetti Bolognese",
                            Price = 12.50m,
                            RestaurantId = 2
                        });
                });

            modelBuilder.Entity("FOABE.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            OrderDate = new DateTime(2024, 10, 14, 23, 42, 45, 881, DateTimeKind.Local).AddTicks(2560),
                            OrderStatus = "Placed",
                            RestaurantId = 1,
                            UserId = 2
                        },
                        new
                        {
                            OrderId = 2,
                            OrderDate = new DateTime(2024, 10, 14, 23, 42, 45, 881, DateTimeKind.Local).AddTicks(2561),
                            OrderStatus = "Preparing",
                            RestaurantId = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FOABE.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantId"), 1L, 1);

                    b.Property<string>("Cuisine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RestaurantId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            Cuisine = "American",
                            Location = "456 Diner Street, Foodville",
                            OwnerId = 3,
                            RestaurantName = "John's Diner"
                        },
                        new
                        {
                            RestaurantId = 2,
                            Cuisine = "Italian",
                            Location = "789 Pasta Lane, Foodtown",
                            OwnerId = 3,
                            RestaurantName = "Jane's Italian Eatery"
                        });
                });

            modelBuilder.Entity("FOABE.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("DishId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            ReviewId = 1,
                            DishId = 1,
                            Rating = 5,
                            RestaurantId = 1,
                            ReviewText = "Amazing cheeseburger!",
                            UserId = 2
                        },
                        new
                        {
                            ReviewId = 2,
                            DishId = 3,
                            Rating = 4,
                            RestaurantId = 2,
                            ReviewText = "Delicious pasta, but could use a bit more sauce.",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FOABE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternativeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PinCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "AP",
                            AlternativeNumber = "12345",
                            City = "Kadapa",
                            CreatedAt = new DateTime(2024, 6, 11, 13, 28, 12, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            MobileNumber = "1234567890",
                            Password = "admin1234",
                            PinCode = "12345",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2024, 7, 3, 9, 20, 12, 0, DateTimeKind.Unspecified),
                            UserType = "Admin",
                            userName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Address = "AP",
                            AlternativeNumber = "12345",
                            City = "Kurnool",
                            CreatedAt = new DateTime(2024, 3, 6, 13, 30, 12, 0, DateTimeKind.Unspecified),
                            Email = "sai@gmail.com",
                            MobileNumber = "1234567890",
                            Password = "sai1234",
                            PinCode = "54321",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2024, 6, 12, 9, 20, 12, 0, DateTimeKind.Unspecified),
                            UserType = "Customer",
                            userName = "sai"
                        },
                        new
                        {
                            Id = 3,
                            Address = "AP",
                            AlternativeNumber = "12345",
                            City = "Guntur",
                            CreatedAt = new DateTime(2024, 1, 6, 14, 30, 12, 0, DateTimeKind.Unspecified),
                            Email = "manu@gmail.com",
                            MobileNumber = "1234567890",
                            Password = "manu4321",
                            PinCode = "33333",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2024, 9, 3, 9, 20, 12, 0, DateTimeKind.Unspecified),
                            UserType = "Customer",
                            userName = "manu"
                        },
                        new
                        {
                            Id = 4,
                            Address = "AP",
                            AlternativeNumber = "12345",
                            City = "Anantpur",
                            CreatedAt = new DateTime(2024, 3, 6, 20, 40, 12, 0, DateTimeKind.Unspecified),
                            Email = "rani@gmail.com",
                            MobileNumber = "1234567890",
                            Password = "rani43",
                            PinCode = "55555",
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2024, 6, 3, 9, 20, 12, 0, DateTimeKind.Unspecified),
                            UserType = "Customer",
                            userName = "Rani"
                        });
                });

            modelBuilder.Entity("FOABE.Models.Cart", b =>
                {
                    b.HasOne("FOABE.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("FOABE.Models.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FOABE.Models.CartItem", b =>
                {
                    b.HasOne("FOABE.Models.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FOABE.Models.Menu", "Menu")
                        .WithMany("CartItems")
                        .HasForeignKey("MenuId");

                    b.Navigation("Cart");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("FOABE.Models.Menu", b =>
                {
                    b.HasOne("FOABE.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("FOABE.Models.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("FOABE.Models.Order", b =>
                {
                    b.HasOne("FOABE.Models.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FOABE.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FOABE.Models.Restaurant", b =>
                {
                    b.HasOne("FOABE.Models.User", "User")
                        .WithMany("RestaurantsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FOABE.Models.Review", b =>
                {
                    b.HasOne("FOABE.Models.Menu", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FOABE.Models.Restaurant", "Restaurant")
                        .WithMany("Reviews")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FOABE.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FOABE.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("FOABE.Models.Menu", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("FOABE.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("FOABE.Models.Restaurant", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("FOABE.Models.User", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");

                    b.Navigation("RestaurantsOwned");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
