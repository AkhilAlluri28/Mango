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
        [NotMapped]
        public decimal Discount { get; set; }

        /// <summary>
        /// CartTotal calculated on-demand
        /// </summary>
        [NotMapped]
        public decimal CartTotal { get; set; }
    }
}
