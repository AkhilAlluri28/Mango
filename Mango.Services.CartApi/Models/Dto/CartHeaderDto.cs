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
        public int CartHeaderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        ///  Discount calculated on-demand
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// CartTotal calculated on-demand
        /// </summary>
        public decimal CartTotal { get; set; }
    }
}
