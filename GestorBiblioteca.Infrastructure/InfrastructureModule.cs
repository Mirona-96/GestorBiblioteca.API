using GestorBiblioteca.Core.Repositories;
using GestorBiblioteca.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services)
        {

            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories (this IServiceCollection services)
        {
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

            return services;
        }
    }
}
