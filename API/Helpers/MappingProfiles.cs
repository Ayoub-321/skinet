using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using API.Dtos;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Adress, AdressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}