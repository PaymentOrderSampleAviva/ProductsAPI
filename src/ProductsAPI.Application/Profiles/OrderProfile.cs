using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.Profiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderDto>()
				.ForMember(x => x.Status, y => y.MapFrom(z => z.Status));

			CreateMap<CreateOrderDto, Order>()
				.ForMember(x => x.PaymentMethod, y => y.MapFrom(z => z.Method))
				.ForMember(x => x.Items, y => y.MapFrom(z => z.Products));

		}
	}
}
