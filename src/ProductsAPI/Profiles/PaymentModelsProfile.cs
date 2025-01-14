﻿using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;
using ProductsAPI.PaymentProcessors.Model;

namespace ProductsAPI.Profiles
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
