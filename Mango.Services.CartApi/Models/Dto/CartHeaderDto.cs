namespace Mango.Services.CartApi.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public record CartHeaderDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int CartHeaderId { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string? CouponCode { get; init; }

        /// <summary>
        ///  Discount calculated on-demand
        /// </summary>
        public decimal Discount { get; init; }

        /// <summary>
        /// CartTotal calculated on-demand
        /// </summary>
        public decimal CartTotal { get; init; }
    }
}
