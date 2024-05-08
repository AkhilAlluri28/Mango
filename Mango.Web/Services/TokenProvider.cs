using Mango.Web.Services.Interfaces;
using Mango.Web.Utilities;

namespace Mango.Web.Services
{
    public class TokenProvider(IHttpContextAccessor httpContextAccessor) : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetails.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            _ = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);
            return token;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token);
        }
    }
}
