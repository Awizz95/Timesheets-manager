using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Infrastructure.Mappers;
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
            User? user = await _userManager.GetUserById(userId);

            if (user is null) return BadRequest("Пользователь с этим id не найден");

            string json = JsonSerializer.Serialize(user);

            return Ok(json);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users = await _userManager.GetAll();

            if (!users.Any()) return Ok("Список пользователей пуст");

            string json = JsonSerializer.Serialize(users);

            return Ok(json);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (request is null) return BadRequest("Пустой запрос");

            Guid userId;
            try
            {
                userId = await _userManager.CreateUser(request);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(userId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            if (request is null) return BadRequest("Пустой запрос");

            bool isRequestCorrect = await _userManager.CheckUpdateUserRequest(request);

            if (!isRequestCorrect) return BadRequest("Запрос некорректен");

            User? userDB = await _userManager.GetUserByEmail(request.Email);
            User user = UserMapper.UpdateUserRequestToUser(userDB.Id, request);

            await _userManager.Update(user);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CheckStatus(string userEmail)
        {
            bool isDeleted = await _userManager.CheckUserIsDeleted(userEmail);

            if (isDeleted) return Ok("Статус пользователя \"Удален\"");
            else return Ok("Статус пользователя \"Не удален\"");
        }
    }
}
