using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi.Interfaces
{
    /// <summary>
    /// Fetches the coupons related info from DB.
    /// </summary>
    public interface ICouponRepository
    {

        /// <summary>
        /// Retrieves all the coupons.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Coupon> GetAllCoupons();

        /// <summary>
        ///  Retrieves the coupon of given id.
        /// </summary>
        /// <param name="couponId">Unique identifier of coupon.</param>
        /// <returns>Coupon of given id.</returns>
        public Coupon? GetCouponById(int couponId);

        /// <summary>
        ///  Retrieves the coupon of given code.
        /// </summary>
        /// <param name="code">code of coupon.</param>
        /// <returns>Coupon of given code.</returns>
        public Coupon? GetCouponByCode(string code);

        /// <summary>
        /// Creates coupon.
        /// </summary>
        /// <param name="coupon"></param>
        public void CreateCoupon(Coupon coupon);

        /// <summary>
        /// Updates coupon.
        /// </summary>
        /// <param name="coupon"></param>
        public void UpdateCoupon(Coupon coupon);

        /// <summary>
        /// Deletes Coupon of given id.
        /// </summary>
        /// <param name="couponId">Unique identifier of coupon.</param>
        public void DeleteCoupon(int couponId);
    }
}
