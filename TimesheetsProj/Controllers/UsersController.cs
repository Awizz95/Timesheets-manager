using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[Action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid userId)
        {
            User? user = await _userManager.GetUserById(userId);

            if (user is null) return NotFound();

            var json = JsonSerializer.Serialize(user);

            return Ok(json);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            request.EnsureNotNull(nameof(request));
            var response = await _userManager.CreateUser(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid userId, [FromBody] UpdateUserRequest request)
        {
            request.EnsureNotNull(nameof(request));
            await _userManager.Update(userId, request);

            return Ok();
        }
    }
}
