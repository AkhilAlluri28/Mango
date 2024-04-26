using Mango.Web.Enums;

namespace Mango.Web.Utilities
{
    public class StaticDetails
    {
        public const string TokenCookie = "JwtToken";
        public static string RoleAdmin = Role.ADMIN.ToString();
        public static string RoleCutomer = Role.CUSTOMER.ToString();
        public static string CouponApiBaseUrl { get; set; } = null!;
        public static string AuthApiBaseUrl { get; set; } = null!;
    }
}
