namespace TimesheetsProj.Models.Dto.Requests
{
    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
    }
}
