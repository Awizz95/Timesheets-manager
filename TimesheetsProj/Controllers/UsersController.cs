using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[Action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] Guid userId)
        {
            User? user;

            try
            {
                user = await _userManager.GetUserById(userId);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            var json = JsonSerializer.Serialize(user);

            return Ok(json);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            request.EnsureNotNull(nameof(request));
            Guid response;

            try
            {
                response = await _userManager.CreateUser(request);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromQuery] Guid userId, [FromBody] UpdateUserRequest request)
        {
            request.EnsureNotNull(nameof(request));

            try
            {
                await _userManager.Update(userId, request);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
