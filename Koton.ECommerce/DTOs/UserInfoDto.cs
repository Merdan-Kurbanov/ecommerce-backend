namespace Koton.ECommerce.Core.DTOs
{

    public class UserInfoDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }

}
