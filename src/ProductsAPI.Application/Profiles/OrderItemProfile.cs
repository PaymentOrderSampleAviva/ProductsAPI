using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.Profiles
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
