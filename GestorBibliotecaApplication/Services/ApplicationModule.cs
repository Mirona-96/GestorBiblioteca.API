using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services
                .AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            //services.AddMediatR(typeof();

            return services;
        }
    }
}
