namespace Mango.Services.CartApi.Models.Dto
{
    public record CartDetailsDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int CartDetailsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public CartHeaderDto? CartHeader { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductDto? Product { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
    }
}
