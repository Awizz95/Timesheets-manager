using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Dto.Responses;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager loginManager, IUserManager userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            User user;

            try
            {
                request.EnsureNotNull(nameof(request));
                user = await _userManager.GetUserByRequest(request);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            LoginResponse loginResponse = await _loginManager.Authenticate(user);

            return Ok(loginResponse);
        }

        //public Task<IActionResult> Refresh()
        //{
        //    var rub100 = Money.FromDecimal(100);

        //    return null;
        //}
    }
}
