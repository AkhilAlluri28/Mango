using Mango.Services.CartApi.Models.Dto;
using Mango.Services.CartApi.Services.IServices;
using Newtonsoft.Json;

namespace Mango.Services.CartApi.Services
{
    public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
    {
        public readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/products");

            if (response.IsSuccessStatusCode)
            {
                string apiContent = await response.Content.ReadAsStringAsync();
                ResponseDto responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                if (responseDto.IsSuccess)
                    return JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Body));
            }
            return new List<ProductDto>();
        }
    }
}
