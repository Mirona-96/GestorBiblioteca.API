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
    public interface IAuthService
    {
        string ComputeHash(string password);
        string GenerateToken(string email, string role);
    }
}
