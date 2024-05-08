using Mango.Web.Models.Auth;

namespace Mango.Web.Services.Interfaces
{
    /// <summary>
    /// Auth Service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// For registering new user.
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> RegisterUserAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Logging in existing user.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Assign roles like ADMIN, Customer etc roles to an user.
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        public Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
