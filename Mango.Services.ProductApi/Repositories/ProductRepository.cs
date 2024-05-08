using Mango.Services.ProductApi.Data;
using Mango.Services.ProductApi.Interfaces;
using Mango.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Repositories
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        ///<inherit />
        public void Create(Product product)
        {
            if(_dbContext.Products.Any(p=> p.ProductId == product.ProductId))
            {
                return;
            }
            else
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
            }
        }

        ///<inherit />
        public void Delete(int productId)
        {
            Product? product = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        ///<inherit />
        public Product? GetProductById(int productId)
        {
            return _dbContext.Products.AsNoTracking().FirstOrDefault(p=> p.ProductId == productId);
        }

        ///<inherit />
        public List<Product> GetAllProducts()
        {
            return [.. _dbContext.Products.AsNoTracking()];
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
