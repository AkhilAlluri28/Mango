using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    /// <inherit/>
    public class CouponService(IBaseService baseService) : ICouponService
    {
        private readonly IBaseService _baseService = baseService;
        private const string CouponApiBaseUrl = "https://localhost:7001/api/coupons";

        /// <inherit/>
        public async Task<ResponseDto> GetAllCoupons()
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = CouponApiBaseUrl
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> GetCouponById(int couponId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = CouponApiBaseUrl + $"/{couponId}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> GetCouponByCode(string code)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = CouponApiBaseUrl + $"/by-code/{code}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> CreateCoupon(CouponDto couponDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Post,
                Url = CouponApiBaseUrl,
                Body = couponDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> UpdateCoupon(CouponDto couponDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Put,
                Url = CouponApiBaseUrl + $"/{couponDto.CouponId}",
                Body = couponDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> DeleteCoupon(int couponId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Delete,
                Url = CouponApiBaseUrl + $"/{couponId}"
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
