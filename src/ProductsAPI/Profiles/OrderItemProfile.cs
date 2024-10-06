using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;

namespace ProductsAPI.Profiles
{
	public class OrderItemProfile : Profile
	{
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
		}
    }
}
