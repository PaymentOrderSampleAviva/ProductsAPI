using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;

namespace ProductsAPI.Profiles
{
	public class OrderItemProfile : Profile
	{
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.Details, y => y.MapFrom(z => z.Product.Details));

			CreateMap<OrderItemDto, OrderItem>();
		}
    }
}
