﻿namespace TimesheetsProj.Models.Dto.Requests
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
