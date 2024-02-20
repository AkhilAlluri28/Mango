using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Interfaces;
using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi.Repositories
{
    ///<inherit />
    public class CouponRepository(AppDbContext db) : ICouponRepository
    {
        private readonly AppDbContext _db = db;

        ///<inherit />
        public IEnumerable<Coupon> GetAllCoupons()
        {
            return _db.Coupons.ToList();
        }

        ///<inherit />
        public Coupon? GetCouponById(int couponId)
        {
            return _db.Coupons.FirstOrDefault(c => c.CouponId == couponId);
        }

        /// <inherit />
        public Coupon? GetCouponByCode(string code)
        {
            return _db.Coupons.FirstOrDefault(c=> string.Equals(c.CouponCode, code, 
                StringComparison.OrdinalIgnoreCase));
        }

        /// <inherit />
        public void CreateCoupon(Coupon coupon)
        {
            _db.Coupons.Add(coupon);
            _db.SaveChanges();
        }

        /// <inherit />
        public void UpdateCoupon(Coupon coupon)
        {
            _db.Coupons.Update(coupon);
            _db.SaveChanges();
        }

        /// <inherit />
        public void DeleteCoupon(int couponId)
        {
            Coupon? couponToRemove = _db.Coupons.FirstOrDefault(c => c.CouponId == couponId);
            if(couponToRemove != null)
            {
                _db.Coupons.Remove(couponToRemove);
                _db.SaveChanges();
            }
        }
    }
}
