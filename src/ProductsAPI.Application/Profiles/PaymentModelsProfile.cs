using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Entities;
using ProductsAPI.PaymentsGateway.Model;

namespace ProductsAPI.Application.Profiles
{
	public class PaymentModelsProfile : Profile
	{
        public PaymentModelsProfile()
        {
            CreateMap<CreateOrderDto, CreateOrderModel>()
                .ForMember(x => x.Method, y => y.MapFrom(z => z.Method));

            CreateMap<OrderItemDto, ProductModel>();

            CreateMap<FeeModel, OrderFee>();
        }
    }
}
