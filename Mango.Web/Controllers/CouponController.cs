using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        private readonly ICouponService _couponService = couponService;

        public async Task<IActionResult> CouponIndex()
        {
            ResponseDto responseDto = await _couponService.GetAllCouponsAsync();
            if (responseDto?.IsSuccess ?? false)
            {
                IEnumerable<CouponDto> couponsList = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Body));
                return View(couponsList);
            }
            else
            {
                TempData["error"] = responseDto?.ErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult CouponCreate()
        {
            return View();
        }

        public async Task<IActionResult> CouponSubmit(CouponDto coupon)
        {
            ResponseDto responseDto = await _couponService.CreateAsync(coupon);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Created!";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }
            return RedirectToAction(nameof(CouponCreate));
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto responseDto = await _couponService.DeleteAsync(couponId);
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
