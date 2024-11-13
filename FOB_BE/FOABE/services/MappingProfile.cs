using AutoMapper;
using FOABE.Dtos;
using FOABE.Models;

namespace FOABE.services
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());
            CreateMap<Order, OrderDto>()
           .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId)); // Map the correct field
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
          .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Menu.DishName))
          .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Menu.Price));

            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}





