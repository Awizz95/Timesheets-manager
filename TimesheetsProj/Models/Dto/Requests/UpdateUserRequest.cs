namespace TimesheetsProj.Models.Dto.Requests
{
    public class UpdateUserRequest
    {
        public required string Email { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string Role { get; set; }
    }
}
 