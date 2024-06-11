namespace TimesheetsProj.Models.Dto.Requests
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
    }
}
