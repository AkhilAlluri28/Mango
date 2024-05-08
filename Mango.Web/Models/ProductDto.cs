using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public record ProductDto
    {
        public int ProductId { get; init; }
        [Required]
        public string Name { get; init; } = null!;
		[Required]
        [Range(0, 1000)]
		public decimal Price { get; init; }

		public string Description { get; init; } = null!;
		public string CategoryName { get; init; } = null!;
		public string ImageUrl { get; init; } = null!;
	}
}
