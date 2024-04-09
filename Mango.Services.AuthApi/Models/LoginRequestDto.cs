using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthApi.Models
{
    /// <summary>
    /// User login details.
    /// </summary>
    public record LoginRequestDto
    {
        /// <summary>
        /// User user name.
        /// </summary>
        [Required]
        public string UserName { get; init; } = null!;

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; init; } = null!;
    }
}
