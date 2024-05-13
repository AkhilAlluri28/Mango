using Mango.Services.CartApi.Models.Dto;

namespace Mango.Services.CartApi.Services.IServices
{
    public interface ICouponService
    {
        public Task<List<CouponDto>> GetAllCoupons();
        public Task<CouponDto> GetCouponByCode(string code);
    }
}
