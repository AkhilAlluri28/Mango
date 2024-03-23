using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        private readonly ICouponService _couponService = couponService;

        public async Task<IActionResult> CouponIndex()
        {
            IEnumerable<CouponDto> couponsList = Enumerable.Empty<CouponDto>();
            ResponseDto responseDto = await _couponService.GetAllCoupons();
            if (responseDto.IsSuccess)
            {
				couponsList = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Body));
            }

            return View(couponsList);
        }

        public IActionResult CreateCoupon()
        {
            return View();
        }

        public async Task<IActionResult> SubmitCoupon(CouponDto coupon)
        {
            await _couponService.CreateCoupon(coupon);
            return RedirectToAction(nameof(CouponIndex));
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            await _couponService.DeleteCoupon(couponId);
            return RedirectToAction(nameof(CouponIndex));
        } 
    }
}
