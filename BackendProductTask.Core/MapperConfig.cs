using AutoMapper;
using BAckendCosmosTask.Domain.Entities;
using BAckendCosmosTask.Domain.Entities.ProductAggregates;
using BAckendCosmosTask.Domain.Models.DTOs;
using BAckendCosmosTask.Domain.Models.Responses;
using BackendProductTask.Domain.Models.Responses;


namespace BAckendCosmosTask.Core
{

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryResponseDto, Category>().ReverseMap();
            CreateMap<ProductResponseDto, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();

            CreateMap<ProductBrandResponseDto, ProductBrand>().ReverseMap();
            CreateMap<ProductImageResponseDto, ProductImage>().ReverseMap();
        }
    }

}
