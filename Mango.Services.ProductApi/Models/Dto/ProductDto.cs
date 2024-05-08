using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductApi.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; init; }

        public string Name { get; init; } = null!;

        public decimal Price { get; init; }

        public string Description { get; init; }
        public string CategoryName { get; init; }
        public string ImageUrl { get; init; }
    }
}
