using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Mango.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;

namespace Mango.Web.Controllers
{
    public class HomeController(IProductService productService, ICartService cartService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly ICartService _cartService = cartService;

        public async Task<IActionResult> Index()
        {
            List<ProductDto> productsList = new();
            ResponseDto responseDto = await _productService.GetAllProductsAsync();

            if (responseDto?.IsSuccess ?? false)
                productsList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Body));
            else
                TempData["error"] = responseDto?.ErrorMessage;

            return View(productsList);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? product = new();
            ResponseDto responseDto = await _productService.GetProductByIdAsync(productId);

            if (responseDto?.IsSuccess ?? false)
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Body));
            else
                TempData["error"] = responseDto?.ErrorMessage;

            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            CartDetailsDto cartDetails = new CartDetailsDto
            {
                Count = productDto.Quantity,
                ProductId = productDto.ProductId
            };

            CartDto cartDto = new()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject).FirstOrDefault().Value
                },
                CartDetails = new List<CartDetailsDto> { cartDetails }
            };

            var responseDto = await _cartService.UpsertCartAsync(cartDto);

            if(responseDto?.IsSuccess ?? false)
            {
                TempData["success"] = "Item has been added!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.ErrorMessage;
            }
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
