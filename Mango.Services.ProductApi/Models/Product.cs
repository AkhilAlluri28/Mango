using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductApi.Models
{
    public record Product
    {
        [Key]
        public int ProductId { get; init; }

        [Required]
        public string Name { get; init; } = null!;

        [Range(0, 1000)]
        public decimal Price { get; init; }

        public string Description { get; init; } = null!;
        public string CategoryName { get; init; } = null!;
        public string ImageUrl { get; init; } = null!;

    }
}
