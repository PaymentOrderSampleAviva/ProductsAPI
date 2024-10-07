using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Extensions;
using ProductsAPI.Models;

namespace ProductsAPI.Profiles
{
	public class ProductProfile : Profile
	{
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.StatusId, y => y.MapFrom(z => z.Status))
				.ForMember(x => x.Status, y => y.MapFrom(z => z.Status.GetLocalizedValue()));
		}
    }
}
