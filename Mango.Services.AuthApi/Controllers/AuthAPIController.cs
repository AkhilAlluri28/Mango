using Mango.Services.AuthApi.Interfaces;
using Mango.Services.AuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Mango.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    public class AuthAPIController : Controller
    {
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto> Register([FromBody]RegistrationRequestDto registrationDto)
        {
            var resultMessage = await _authService.RegisterUser(registrationDto);
            if (!resultMessage.IsNullOrEmpty())
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = resultMessage
                };
            }
            bool isAssignRoleSuccessful = await _authService.AssignRole(registrationDto.Email, registrationDto.Role ?? "CUSTOMER");
            if (!isAssignRoleSuccessful)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Failed to assign role to this user."
                };
            }
            return new ResponseDto
            {
                StatusCode = HttpStatusCode.NoContent,
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseDto> Login([FromBody] LoginRequestDto loginDto)
        {
            LoginResponseDto loginResponseDto = await _authService.UserLogin(loginDto);

            if(loginResponseDto.User == null)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessage = "Username or password is incorrect."
                };
            }

            return new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                Body = loginResponseDto
            };
        }

        [HttpPost]
        [Route("user-role")]
        public async Task<ResponseDto> AssignRole([FromBody]RegistrationRequestDto registrationDto)
        {
            var isAssignRoleSuccessful = await _authService.AssignRole(registrationDto.Email, registrationDto.Role?.ToUpper());

            if (isAssignRoleSuccessful)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NoContent,
                };
            }
            return new ResponseDto
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "Role assignment failed."
            };
        }
    }
}
