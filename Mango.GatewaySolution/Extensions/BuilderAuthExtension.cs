using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.GatewaySolution.Extensions
{
    public static class BuilderAuthExtension
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var apiSettings = builder.Configuration.GetSection("ApiSettings");

            var secretKey = apiSettings.GetValue<string>("secret");
            var issuer = apiSettings.GetValue<string>("Issuer");
            var audience = apiSettings.GetValue<string>("Audience");

            var hashedKey = Encoding.ASCII.GetBytes(secretKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(hashedKey),

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience
                };
            });

            return builder;
        }
    }
}
