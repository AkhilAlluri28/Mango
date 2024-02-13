namespace Mango.Services.CouponApi.Models.Dto
{
    public record CouponDto
    {
        public int CouponId { get; init; }
        public string CouponCode { get; init; } = null!;

        public decimal DiscountAmount { get; init; }
        public decimal MinAmount { get; init; }
    }
}
