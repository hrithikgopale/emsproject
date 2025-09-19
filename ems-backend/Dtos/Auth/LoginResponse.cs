namespace ems_backend.Dtos.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
