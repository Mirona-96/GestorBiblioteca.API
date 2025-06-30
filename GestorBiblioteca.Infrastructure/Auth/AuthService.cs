using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string IAuthService.ComputeHash(string password)
        {
            using (var hash = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);

                var hashBytes = hash.ComputeHash(passwordBytes);

                var builder = new StringBuilder();

                for (var i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        string IAuthService.GenerateToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("username", email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(2), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
