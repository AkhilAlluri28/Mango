namespace Mango.Services.CartApi.Models.Dto
{
    public record CartDto
    {
        /// <summary>
        /// 
        /// </summary>
        public CartHeaderDto CartHeader { get; init; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CartDetailsDto> CartDetails { get; init; } = Enumerable.Empty<CartDetailsDto>();
    }
}
