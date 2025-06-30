using GestorBiblioteca.Core.Repositories;
using GestorBiblioteca.Infrastructure.Auth;
using GestorBiblioteca.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Persistence
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services)
        {

            services.AddRepositories();

            //services.AddAuth();

            return services;
        }

        public static IServiceCollection AddRepositories (this IServiceCollection services)
        {
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

            return services;
        }

        private static IServiceCollection AddAuth (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwy:Key"]))
                    };
                });

            return services;
        }
    }
}
