namespace venkat.Common.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserGuid { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string RoleName { get; set; }
    }
}