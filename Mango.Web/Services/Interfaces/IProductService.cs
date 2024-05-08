using Mango.Web.Models;

namespace Mango.Web.Services.Interfaces
{
    /// <summary>
    /// Provides api service for ProductApi.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all the Products.
        /// </summary>
        /// <returns></returns>
        public Task<ResponseDto> GetAllProductsAsync();

        /// <summary>
        ///  Retrieves the Product of given id.
        /// </summary>
        /// <param name="ProductId">Unique identifier of Product.</param>
        /// <returns>Product of given id.</returns>
        public Task<ResponseDto> GetProductByIdAsync(int ProductId);

        /// <summary>
        /// Creates Product.
        /// </summary>
        /// <param name="ProductDto"><see cref="ProductDto"/></param>
        public Task<ResponseDto> CreateAsync(ProductDto ProductDto);

        /// <summary>
        /// Updates Product.
        /// </summary>
        /// <param name="ProductDto"><see cref="ProductDto"/></param>
        public Task<ResponseDto> UpdateAsync(ProductDto ProductDto);

        /// <summary>
        /// Deletes Product of given id.
        /// </summary>
        /// <param name="ProductId">Unique identifier of Product.</param>
        public Task<ResponseDto> DeleteAsync(int ProductId);
    }
}
