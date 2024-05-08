using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Interfaces;
using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Repositories
{
    ///<inherit />
    public class CouponRepository(AppDbContext db) : ICouponRepository
    {
        private readonly AppDbContext _dbContext = db;

        ///<inherit />
        public IEnumerable<Coupon> GetAllCoupons()
        {
            return _dbContext.Coupons.AsNoTracking().ToList();
        }

        ///<inherit />
        public Coupon? GetCouponById(int couponId)
        {
            return _dbContext.Coupons.AsNoTracking().FirstOrDefault(c => c.CouponId == couponId);
        }

        /// <inherit />
        public Coupon? GetCouponByCode(string code)
        {
            //return _db.Coupons.FirstOrDefault(c=> string.Equals(c.CouponCode, code, 
            //    StringComparison.OrdinalIgnoreCase));
            return _dbContext.Coupons.AsNoTracking().FirstOrDefault(c => c.CouponCode == code);
        }

        /// <inherit />
        public void Create(Coupon coupon)
        {
            _dbContext.Coupons.Add(coupon);
            _dbContext.SaveChanges();
        }

        /// <inherit />
        public void Update(Coupon coupon)
        {
            _dbContext.Coupons.Update(coupon);
            _dbContext.SaveChanges();
        }

        /// <inherit />
        public void Delete(int couponId)
        {
            Coupon? couponToRemove = _dbContext.Coupons.AsNoTracking().FirstOrDefault(c => c.CouponId == couponId);
            if(couponToRemove != null)
            {
                _dbContext.Coupons.Remove(couponToRemove);
                _dbContext.SaveChanges();
            }
        }
    }
}
