using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;

namespace ProductsAPI.Profiles
{
    public class OrderFeeProfile : Profile
    {
        public OrderFeeProfile()
        {
            CreateMap<OrderFee, OrderFeeDto>();
        }
    }
}
