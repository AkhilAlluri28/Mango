using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.Auth
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
        [MinLength(8)]
        public string Password { get; init; } = null!;
    }
}
