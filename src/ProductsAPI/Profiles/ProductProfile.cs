using AutoMapper;
using ProductsAPI.DataContracts;
using ProductsAPI.Extensions;
using ProductsAPI.Models;

namespace ProductsAPI.Profiles
{
	public class ProductProfile : Profile
	{
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(x => x.Status, y => y.MapFrom(z => z.Status.GetLocalizedValue()));
        }
    }
}
