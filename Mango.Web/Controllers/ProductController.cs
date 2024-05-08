using Mango.Web.Models;
using Mango.Web.Services;
using Mango.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController(IProductService ProductService) : Controller
    {
        private readonly IProductService _ProductService = ProductService;

        public async Task<IActionResult> ProductIndex()
        {
            ResponseDto responseDto = await _ProductService.GetAllProductsAsync();
            if (responseDto?.IsSuccess ?? false)
            {
                IEnumerable<ProductDto> ProductsList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Body));
                return View(ProductsList);
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

        public async Task<IActionResult> ProductCreateSubmit(ProductDto Product)
        {
            ResponseDto responseDto = await _ProductService.CreateAsync(Product);
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
            ResponseDto responseDto = await _ProductService.GetProductByIdAsync(productId);
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

        public async Task<IActionResult> ProductEditSubmit(ProductDto Product)
        {
            ResponseDto responseDto = await _ProductService.UpdateAsync(Product);
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

        public async Task<IActionResult> ProductDelete(int ProductId)
        {
            ResponseDto responseDto = await _ProductService.DeleteAsync(ProductId);
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
