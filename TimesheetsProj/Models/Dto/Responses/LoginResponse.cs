namespace TimesheetsProj.Models.Dto.Responses
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }
    }
}
