namespace Mango.Web.Models.Auth
{
    /// <summary>
    /// Login Repsonse.
    /// </summary>
    public record LoginResponseDto
    {
        /// <summary>
        /// User info like name, id, email, and phone number.
        /// </summary>
        public UserDto? User { get; init; }

        /// <summary>
        /// JWT token generated for loggedin user.
        /// </summary>
        public string Token { get; init; } = null!;
    }
}
