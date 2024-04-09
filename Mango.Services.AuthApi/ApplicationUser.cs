using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthApi
{
    /// <summary>
    /// Custom columns for a user identity.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User Name.
        /// </summary>
        public string Name { get; init; } = null!;
    }
}
