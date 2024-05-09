using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.CartApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record CartHeader
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
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
        [NotMapped]
        public decimal Discount { get; init; }

        /// <summary>
        /// CartTotal calculated on-demand
        /// </summary>
        [NotMapped]
        public decimal CartTotal { get; init; }
    }
}
