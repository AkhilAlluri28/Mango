using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    /// <summary>
    /// Provides api service for CouponApi.
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// Retrieves all the coupons.
        /// </summary>
        /// <returns></returns>
        public Task<ResponseDto> GetAllCoupons();

        /// <summary>
        ///  Retrieves the coupon of given id.
        /// </summary>
        /// <param name="couponId">Unique identifier of coupon.</param>
        /// <returns>Coupon of given id.</returns>
        public Task<ResponseDto> GetCouponById(int couponId);

        /// <summary>
        ///  Retrieves the coupon of given code.
        /// </summary>
        /// <param name="code">code of coupon.</param>
        /// <returns>Coupon of given code.</returns>
        public Task<ResponseDto> GetCouponByCode(string code);

        /// <summary>
        /// Creates coupon.
        /// </summary>
        /// <param name="couponDto"><see cref="CouponDto"/></param>
        public Task<ResponseDto> CreateCoupon(CouponDto couponDto);

        /// <summary>
        /// Updates coupon.
        /// </summary>
        /// <param name="couponDto"><see cref="CouponDto"/></param>
        public Task<ResponseDto> UpdateCoupon(CouponDto couponDto);

        /// <summary>
        /// Deletes Coupon of given id.
        /// </summary>
        /// <param name="couponId">Unique identifier of coupon.</param>
        public Task<ResponseDto> DeleteCoupon(int couponId);
    }
}
