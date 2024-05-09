using AutoMapper;
using Mango.Services.CartApi.Models;
using Mango.Services.CartApi.Models.Dto;

namespace Mango.Services.CartApi;
public class MappingConfig
{
    public static MapperConfiguration RegisterMapping() 
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        });
    }
}
