using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        private readonly ICartService _cartService = cartService;

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartBasedOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            ResponseDto respionseDto = await _cartService.RemoveFromCartAsync(cartDetailsId);

            if (respionseDto?.IsSuccess ?? false)
            {
                TempData["success"] = "Cart updated succesfully!";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto respionseDto = await _cartService.ApplyCouponAsync(cartDto);

            if (respionseDto.IsSuccess)
            {
                TempData["success"] = "Cart updated succesfully!";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = string.Empty;
            ResponseDto respionseDto = await _cartService.ApplyCouponAsync(cartDto);

            if (respionseDto.IsSuccess)
            {
                TempData["success"] = "Cart updated succesfully!";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EmailCart()
        {
            var cart = await LoadCartBasedOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email).FirstOrDefault()?.Value;

            ResponseDto respionseDto = await _cartService.EmailCartAsync(cart);

            if (respionseDto.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly!";
                return RedirectToAction(nameof(CartIndex));
            }

            return RedirectToAction(nameof(CartIndex));
        }


        private async Task<CartDto> LoadCartBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u =>
                u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;

            ResponseDto respionseDto = await _cartService.GetCartByUserIdAsync(userId);

            if (respionseDto.IsSuccess)
            {
                return
                    JsonConvert.DeserializeObject<CartDto>(Convert.ToString(respionseDto.Body));
            }
            
            return new CartDto();
        }
    }
}
