using Dapper;
using Koton.ECommerce.Core.DTOs;
using Koton.ECommerce.DataAccess.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Koton.ECommerce.DataAccess.Repositories.Concrete
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _config;

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<UserInfoDto> GetUserInfoAsync(string username, string password)
        {
            var connStr = _config.GetSection("ConnectionStrings:ConnStr").Value;

            using (var connection = new SqlConnection(connStr))
            {
                // Filter the query based on the provided username and password.
                var userInfo = await connection.QueryFirstOrDefaultAsync<UserInfoDto>(
                    sql: @"SELECT u.Username, u.PasswordHash, u.Email, r.Code as RoleCode, r.Name as RoleName
                   FROM Users u
                   INNER JOIN Roles r ON u.RoleId = r.RoleId
                   WHERE u.Username = @Username AND u.PasswordHash = @PasswordHash",
                    param: new { Username = username, PasswordHash = password },
                    commandTimeout: 0);

                return userInfo;
            }
        }

    }
}
