using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.Profiles
{
    public class OrderFeeProfile : Profile
    {
        public OrderFeeProfile()
        {
            CreateMap<OrderFee, OrderFeeDto>();
        }
    }
}
