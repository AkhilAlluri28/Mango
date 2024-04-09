namespace Mango.Services.AuthApi.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates Jwt token from application user info.
        /// </summary>
        /// <returns>Jwt Token.</returns>
        public string GenerateToken(ApplicationUser user);
    }
}
