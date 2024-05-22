using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto;
using TimesheetsProj.Models.Dto.Authentication;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class LoginManager : ILoginManager
    {
        private readonly JwtAccessOptions _jwtAccessOptions;

        public LoginManager(IOptions<JwtAccessOptions> jwtAccessOptions)
        {
            _jwtAccessOptions = jwtAccessOptions.Value;
        }

        public async Task<LoginResponse> Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var accessTokenRaw = _jwtAccessOptions.GenerateToken(claims);
            var securityHandler = new JwtSecurityTokenHandler();
            var accessToken = securityHandler.WriteToken(accessTokenRaw);

            var loginResponse = new LoginResponse()
            {
                AccessToken = accessToken,
                ExpiresIn = accessTokenRaw.ValidTo.ToEpochTime()
            };

            return loginResponse;
        }
    }
}
