using Mango.Web.Models;
using Mango.Web.Services;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        public async Task<IActionResult> ProductIndex()
        {
            ResponseDto responseDto = await _productService.GetAllProductsAsync();
            if (responseDto?.IsSuccess ?? false)
            {
                IEnumerable<ProductDto> productsList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Body));
                return View(productsList);
            }
            else
            {
                TempData["error"] = responseDto?.ErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        public async Task<IActionResult> ProductCreateSubmit(ProductDto product)
        {
            ResponseDto responseDto = await _productService.CreateAsync(product);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Created!";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }
            return RedirectToAction(nameof(ProductCreate));
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto responseDto = await _productService.GetProductByIdAsync(productId);
            if (responseDto?.IsSuccess ?? false)
            {
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Body));
                return View(productDto);
            }
            else
            {
                TempData["error"] = responseDto?.ErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ProductEditSubmit(ProductDto product)
        {
            ResponseDto responseDto = await _productService.UpdateAsync(product);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Updated!";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }
            return RedirectToAction(nameof(ProductEdit));
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto responseDto = await _productService.DeleteAsync(productId);
            if (responseDto.IsSuccess)
            {
                TempData["success"] = "Deleted!";
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }

            return RedirectToAction(nameof(ProductIndex));
        }
    }
}
