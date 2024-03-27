using HotelBookingSystem.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingSystem.Authendication
{
    public class Authendicate : IAuthendicate
    {
        public Authendicate(IConfiguration configuration)
        {
            Key = configuration["Jwt:Key"] ?? throw new Exception("Data not found");
            string expTimeinMin = configuration["jwt:extime"] ?? throw new Exception("Data not found");
            ExpMin = int.TryParse(expTimeinMin,out int newExpMin) ? newExpMin : throw new Exception("Data not found");
        }

        public string Key { get; }

        private int ExpMin { get; } = 5;

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Type),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("FirstName",user.FirstName),
                    new Claim("FirstName",user.LastName),
                    new Claim("Age",user.Age.ToString()),
                    new Claim("Id",user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(ExpMin),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string Token = tokenHandler.WriteToken(token);

            return Token;
        }
    }
}
