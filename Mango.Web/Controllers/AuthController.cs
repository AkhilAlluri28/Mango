using Mango.Web.Models.Auth;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController(IAuthService authService, ITokenProvider tokenProvider) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> SubmitLogin(LoginRequestDto loginRequestDto)
        {
            var responseDto = await _authService.LoginAsync(loginRequestDto);
            if (responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Body));

                await SignInUserAsync(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token ?? string.Empty);
                TempData["success"] = "Authorized!";
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> SubmitRegister(RegistrationRequestDto registrationRequestDto)
        {
            var response = await _authService.RegisterUserAsync(registrationRequestDto);
            if (response.IsSuccess)
            {
                TempData["success"] = "Registered!";
            }
            else
            {
                TempData["error"] = response.ErrorMessage;
                return RedirectToAction("Register");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUserAsync(LoginResponseDto loginResponseDto)
        {
            if (loginResponseDto == null) return;

            JwtSecurityTokenHandler handler = new();

            JwtSecurityToken token = handler.ReadJwtToken(loginResponseDto.Token);

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.Claims.Concat(token.Claims);
            
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                token.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value ??
                string.Empty));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                token.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ??
                string.Empty));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                token.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ??
                string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                token.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value ??
                string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                token.Claims?.FirstOrDefault(c => c.Type == "role")?.Value ??
                string.Empty));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
