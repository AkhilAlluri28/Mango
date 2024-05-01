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
        /// <param name="user">User info</param>
        /// <param name="roles">Roles of respective User</param>
        /// <returns>Generated JWT token.</returns>
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}