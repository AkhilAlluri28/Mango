using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public record ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

		[Required]
        [Range(0, 1000)]
		public decimal Price { get; set; }

		public string Description { get; set; } = null!;

		public string CategoryName { get; set; } = null!;

		public string ImageUrl { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;
	}
}
