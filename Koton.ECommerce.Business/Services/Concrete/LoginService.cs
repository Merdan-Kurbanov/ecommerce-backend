using Koton.ECommerce.Business.Services.Abstract;
using Koton.ECommerce.Core.Common;
using Koton.ECommerce.DataAccess.Repositories.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Koton.ECommerce.Core.DTOs;

namespace Koton.ECommerce.Business.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly string _jwtSecretKey; // Replace this with your secret key.

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }


        public async Task<Result<LoginResultDto>> LoginAsync(string username, string password)
        {
            var userInfo = await _loginRepository.GetUserInfoAsync(username, password);
            if (userInfo != null)
            {
                // User found, generate JWT token and return it as a result.
                var token = GenerateJwtToken(username);
                var loginResult = new LoginResultDto
                {
                    Token = token,
                    RoleCode = userInfo.RoleCode,
                    RoleName = userInfo.RoleName
                };
                return new Result<LoginResultDto> { IsSuccess = true, Data = loginResult, Message = "Success" };
            }
            else
                return new Result<LoginResultDto> { IsSuccess = false, Message = "User Bulunamadı" };
        }


        private string GenerateJwtToken(string username)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("kdjfkkfhdkfhkfjkdfjkdfjkdfjdkjfk");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", "55") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
