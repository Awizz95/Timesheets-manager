using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Authentication;
using TimesheetsProj.Models.Dto.Responses;
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
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            JwtSecurityToken accessTokenRaw = _jwtAccessOptions.GenerateToken(claims);
            JwtSecurityTokenHandler securityHandler = new JwtSecurityTokenHandler();
            string accessToken = securityHandler.WriteToken(accessTokenRaw);

            LoginResponse loginResponse = new LoginResponse()
            {
                Email = user.Email,
                AccessToken = accessToken,
                ExpiresIn = accessTokenRaw.ValidTo.ToEpochTime()
            };

            return loginResponse;
        }
    }
}
