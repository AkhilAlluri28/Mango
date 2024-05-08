using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Mango.Web.Utilities;

namespace Mango.Web.Services
{
    /// <inherit/>
    public class CouponService(IBaseService baseService) : ICouponService
    {
        private readonly IBaseService _baseService = baseService;

        /// <inherit/>
        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.CouponApiBaseUrl + "/api/coupons"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.CouponApiBaseUrl + $"/api/coupons/{couponId}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> GetCouponByCodeAsync(string code)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.CouponApiBaseUrl + $"/api/coupons/by-code/{code}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> CreateAsync(CouponDto couponDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Post,
                Url = StaticDetails.CouponApiBaseUrl + "/api/coupons",
                Body = couponDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> UpdateAsync(CouponDto couponDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Put,
                Url = StaticDetails.CouponApiBaseUrl + $"/api/coupons/{couponDto.CouponId}",
                Body = couponDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> DeleteAsync(int couponId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Delete,
                Url = StaticDetails.CouponApiBaseUrl + $"/api/coupons/{couponId}"
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
