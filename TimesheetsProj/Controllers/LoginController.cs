using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Auth;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public LoginController(IUserManager userManager, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] LoginRequest request)
        {
            if (request is null) return BadRequest("Пустой запрос");

            User? user = await _userManager.GetUserByLoginRequest(request);

            if (user is null) return Unauthorized("Пользователь не найден");

            string aToken = _jwtProvider.GenerateAccessToken(user);
            user.RefreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(_configuration.GetSection("Authentication:JwtOptions:RefreshTokenValidityInHours").Get<int>());

            await _userManager.Update(user);

            HttpContext.Response.Cookies.Append("Timesheets-access-token", aToken);
            HttpContext.Response.Cookies.Append("Timesheets-refresh-token", user.RefreshToken);

            return Ok(new ObjectResult(new
            {
                aToken,
                user.RefreshToken
            }));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAllTokens(string accessToken, string refreshToken)
        {
            if (accessToken is null || refreshToken is null) return BadRequest("access token или refresh token не могут быть пустыми");

            ClaimsPrincipal claims = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);

            if (claims is null) return BadRequest("Неверный access или refresh токен");

            string userEmailAdress = claims.FindFirst(ClaimTypes.Email)!.Value;

            User user;
            try
            {
                user = await _userManager.GetUserByEmail(userEmailAdress) ?? throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                return BadRequest("Неверный access или refresh токен");
            }

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpireTime <= DateTime.UtcNow)
                return BadRequest("Неверный access или refresh токен");

            string newAccessToken = _jwtProvider.GenerateAccessToken(user);
            user.RefreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(_configuration.GetSection("Authentication:JwtOptions:RefreshTokenValidityInHours").Get<int>());

            await _userManager.Update(user);

            HttpContext.Response.Cookies.Append("Timesheets-access-token", newAccessToken);
            HttpContext.Response.Cookies.Append("Timesheets-refresh-token", user.RefreshToken);

            return Ok(new ObjectResult(new
            {
                newAccessToken,
                user.RefreshToken,
                user.RefreshTokenExpireTime
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeRefreshToken(string email)
        {
            User user;
            try
            {
                user = await _userManager.GetUserByEmail(email) ?? throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                return BadRequest("Пользователь с таким email не найден");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpireTime = default;

            await _userManager.Update(user);

            return Ok();
        }
    }
}
