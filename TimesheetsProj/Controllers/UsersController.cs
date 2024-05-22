using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _userManager.CreateUser(request);

            return Ok(response);
        }
    }
}
