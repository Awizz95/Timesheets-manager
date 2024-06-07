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
    [Route("api/[controller]/[Action]")]
    public class UsersController(IUserManager userManager) : ControllerBase
    {
        private readonly IUserManager _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid userId)
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

            string json = JsonSerializer.Serialize(user);

            return Ok(json);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users;

            try
            {
                users = await _userManager.GetAll();
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            string json = JsonSerializer.Serialize(users);

            return Ok(json);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            Guid response;

            try
            {
                request.EnsureNotNull(nameof(request));
                response = await _userManager.CreateUser(request);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid userId, [FromBody] UpdateUserRequest request)
        {
            try
            {
                request.EnsureNotNull(nameof(request));
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
