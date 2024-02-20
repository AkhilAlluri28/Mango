using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponApi.Models
{
    public record Coupon
    {
        [Key]
        public int CouponId { get; init; }

        [Required]
        public string CouponCode { get; init; } = null!;

        [Required]
        public decimal DiscountAmount { get; init; }
        public decimal MinAmount { get; init; }
    }
}
