using Mango.Services.CartApi.Models.Dto;

namespace Mango.Services.CartApi.Services.IServices
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetAllProducts();
    }
}
