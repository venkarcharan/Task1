namespace venkat.Common.DTOs
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string UserGuid { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}