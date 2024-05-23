using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var user = await _userManager.GetUserByRequest(request);

            if (user == null)
            {
                return Unauthorized();
            }

            var loginResponse = await _loginManager.Authenticate(user);

            return Ok(loginResponse);
        }

        //public Task<IActionResult> Refresh()
        //{
        //    var rub100 = Money.FromDecimal(100);

        //    return null;
        //}
    }
}
