using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.Auth
{
    /// <summary>
    /// User registration Details.
    /// </summary>
    public record RegistrationRequestDto
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string Name { get; init; } = null!;

        /// <summary>
        /// User email id
        /// </summary>
        [Required]
        public string Email { get; init; } = null!;

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; init; } = null!;

        /// <summary>
        /// User phone number
        /// </summary>
        public string? PhoneNumber { get; init; }
    }
}
