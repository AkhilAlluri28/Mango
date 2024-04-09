namespace Mango.Services.AuthApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; init; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; init; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; init; } = null!;
    }
}
