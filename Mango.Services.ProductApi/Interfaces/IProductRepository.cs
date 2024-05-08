using Mango.Services.ProductApi.Models;

namespace Mango.Services.ProductApi.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns> List of <see cref="Product"/></returns>
        public List<Product> GetAllProducts();

        /// <summary>
        /// Gets product of given ID.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product? GetProductById(int productId);

        /// <summary>
        /// Creates product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public void Create(Product product);

        /// <summary>
        /// Updates product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public void Update(Product product);

        /// <summary>
        /// Deletes Product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public void Delete(int productId);
    }
}
