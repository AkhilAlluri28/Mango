namespace Mango.Services.AuthApi.Models
{
    public record UserDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Id { get; init; } = null!;

        /// <summary>
        /// User email id
        /// </summary>
        public string Email { get; init; } = null!;

        /// <summary>
        /// User password
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// User phone number
        /// </summary>
        public string? PhoneNumber { get; init; }
    }
}
