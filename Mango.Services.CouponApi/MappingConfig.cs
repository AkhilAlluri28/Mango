using AutoMapper;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;

namespace Mango.Services.CouponApi;
public class MappingConfig
{
    public static MapperConfiguration RegisterMapping() 
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDto>();
            config.CreateMap<CouponDto, Coupon>();
        });
    }
}
