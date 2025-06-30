using FluentValidation;
using FluentValidation.AspNetCore;
using GestorBibliotecaApplication.Commands.CreateEmprestimo;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Notification;
using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
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
                .AddServices()
                .AddHandlers()
                .AddValidation();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<ILivroService, LivroService>();
         //   services.AddScoped<IUsuarioService, UsuarioService>();
            //services.AddMediatR(typeof();

            return services;
        }

        private static IServiceCollection AddHandlers (this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<InsertEmprestimoCommand>());

            services.AddTransient<IPipelineBehavior<InsertEmprestimoCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();

            services.AddMediatR(config =>
               config.RegisterServicesFromAssemblyContaining<EmprestimoCreatedNotification>());

            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertEmprestimoCommand>()
                .AddValidatorsFromAssemblyContaining<CreateUsuarioInputModel>(); ;


            return services;
        }
    }
}
