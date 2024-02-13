namespace Mango.Services.CouponApi.Models
{
    public record Coupon
    {
        public int CouponId { get; init; }
        public string CouponCode { get; init; } = null!;

        public decimal DiscountAmount { get; init; }
        public decimal MinAmount { get; init; }
    }
}
