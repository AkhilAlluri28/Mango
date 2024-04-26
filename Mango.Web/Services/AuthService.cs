using Mango.Web.Models;
using Mango.Web.Models.Auth;
using Mango.Web.Services.IServices;
using Mango.Web.Utilities;

namespace Mango.Web.Services
{
    /// <inherit />
    public class AuthService(IBaseService baseService) : IAuthService
    {
        private readonly IBaseService _baseService = baseService;
        public async Task<ResponseDto> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            var requestDto = new RequestDto
            {
                Url = StaticDetails.AuthApiBaseUrl + "/api/auth/user-role",
                Method = HttpMethod.Post,
                Body = registrationRequestDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var requestDto = new RequestDto
            {
                Url = StaticDetails.AuthApiBaseUrl + "/api/auth/login",
                Method = HttpMethod.Post,
                Body = loginRequestDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto> RegisterUserAsync(RegistrationRequestDto registrationRequestDto)
        {
            var requestDto = new RequestDto
            {
                Url = StaticDetails.AuthApiBaseUrl + "/api/auth/register",
                Method = HttpMethod.Post,
                Body = registrationRequestDto
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
