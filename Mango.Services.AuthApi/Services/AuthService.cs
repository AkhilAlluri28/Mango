using Mango.Services.AuthApi.Data;
using Mango.Services.AuthApi.Interfaces;
using Mango.Services.AuthApi.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <inherit />
        public async Task<string> RegisterUser(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser userToAdd = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                // Heavy lifting like hashing the password will be done by dotnet identity.
                var result = await _userManager.CreateAsync(userToAdd, registrationRequestDto.Password);

                if (result.Succeeded) return string.Empty;

                return result.Errors.FirstOrDefault()?.Description ?? string.Empty;
            }
            catch (Exception) { }
            return "Error Encountered while registering a user.";
        }

        /// <inherit />
        public async Task<LoginResponseDto> UserLogin(LoginRequestDto loginRequestDto)
        {
            var users = _dbContext.ApplicationUsers.ToList();

            var user = users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        
            if(user == null)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = string.Empty
                };
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(!isValid) return new LoginResponseDto() { User = null, Token = string.Empty };

            return new LoginResponseDto()
            {
                User = new UserDto()
                {
                   Id = user.Id,
                   Name = user.Name,
                   Email = user.Email ?? string.Empty,
                   PhoneNumber = user.PhoneNumber
                },
                Token = _jwtTokenGenerator.GenerateToken(user)
            };
        }

        /// <inherit />
        public async Task<bool> AssignRole(string email, string role)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u=> u.Email.ToLower() == email.ToLower());

            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }
    }
}
