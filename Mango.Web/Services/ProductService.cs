using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Mango.Web.Utilities;

namespace Mango.Web.Services
{
    /// <inherit/>
    public class ProductService(IBaseService baseService) : IProductService
    {
        private readonly IBaseService _baseService = baseService;

        /// <inherit/>
        public async Task<ResponseDto> GetAllProductsAsync()
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.ProductApiBaseUrl + "/api/Products"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> GetProductByIdAsync(int productId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.ProductApiBaseUrl + $"/api/Products/{productId}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> CreateAsync(ProductDto productDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Post,
                Url = StaticDetails.ProductApiBaseUrl + "/api/products",
                Body = productDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> UpdateAsync(ProductDto productDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Put,
                Url = StaticDetails.ProductApiBaseUrl + $"/api/Products/{productDto.ProductId}",
                Body = productDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        /// <inherit/>
        public async Task<ResponseDto> DeleteAsync(int productId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Delete,
                Url = StaticDetails.ProductApiBaseUrl + $"/api/Products/{productId}"
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
