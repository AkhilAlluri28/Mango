using Mango.Services.AuthApi.Models;

namespace Mango.Services.AuthApi.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="registrationRequestDto"><see cref="RegistrationRequestDto"></param>
        /// <returns></returns>
        public Task<string> RegisterUser(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Login registered user.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns> Login reponse with user info and token <see cref="LoginResponseDto"></see></returns>
        public Task<LoginResponseDto> UserLogin(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Assigns role to the user.
        /// </summary>
        /// <param name="email">unique identifier of user</param>
        /// <param name="role">role to be asisgned to the user.</param>
        /// <returns></returns>
        public Task<bool> AssignRole(string email, string role);
    }
}
