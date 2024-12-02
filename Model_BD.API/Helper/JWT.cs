using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Model_BD.BAL.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Model_BD.API.Helper
{
    public class CommenModel
    {
        public string Email { get; set; }
        
        public string Role { get; set; }
    }
    public class JWT
    {
        private readonly IConfiguration _config;

        public JWT(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateUserToken(CommenModel model, bool forShortTimeSpan = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            int minutes = Convert.ToInt16(_config["Jwt:ShortTimeSpanInMinutes"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim("Email", model.Email),
                    new Claim("Role", model.Role),
                }),
                Expires = forShortTimeSpan ? DateTime.Now.AddMinutes(minutes) : DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
