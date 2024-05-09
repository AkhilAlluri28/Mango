using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Mango.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Web.Controllers
{
    public class HomeController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

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
