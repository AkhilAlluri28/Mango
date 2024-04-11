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
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }

            return View(couponsList);
        }

        public IActionResult CreateCoupon()
        {
            return View();
        }

        public async Task<IActionResult> SubmitCoupon(CouponDto coupon)
        {
            ResponseDto responseDto = await _couponService.CreateCoupon(coupon);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Created!";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }
            return RedirectToAction(nameof(CreateCoupon));
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            ResponseDto responseDto = await _couponService.DeleteCoupon(couponId);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Deleted!";
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }

            return RedirectToAction(nameof(CouponIndex));
        } 
    }
}
