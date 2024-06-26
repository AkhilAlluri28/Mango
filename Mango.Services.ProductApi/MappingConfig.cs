﻿using AutoMapper;
using Mango.Services.ProductApi.Models;
using Mango.Services.ProductApi.Models.Dto;

namespace Mango.Services.ProductApi;
public class MappingConfig
{
    public static MapperConfiguration RegisterMapping() 
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductDto>().ReverseMap();
        });
    }
}
