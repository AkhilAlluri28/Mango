using Mango.Services.AuthApi.Interfaces;
using Mango.Services.AuthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto registrationDto)
        {
            var resultMessage = await _authService.RegisterUser(registrationDto);
            if (!resultMessage.IsNullOrEmpty())
            {
                var response = new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = resultMessage
                };
                return BadRequest(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            var loginResponse = await _authService.UserLogin(loginDto);

            if(loginResponse.User == null)
            {
                var response = new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Username or password is incorrect."
                };
                return BadRequest(response);
            }
            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("user-email/{email}/role/{role}")]
        public async Task<IActionResult> AssignRole(string email, string role)
        {
            var isAssignRoleSuccessful = await _authService.AssignRole(email, role.ToUpper());

            if (!isAssignRoleSuccessful)
            {
                var response = new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Role assignment failed."
                };
                return BadRequest(response);
            }
            return Ok();
        }
    }
}
