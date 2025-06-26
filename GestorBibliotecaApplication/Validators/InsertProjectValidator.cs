using FluentValidation;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBiblioteca.Infrastructure.Persistence.Repositories;
using GestorBibliotecaApplication.Commands.CreateEmprestimo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Validators
{
    public class InsertProjectValidator: AbstractValidator<InsertEmprestimoCommand>
    {

        private readonly LivrosDbContext _livrosDbContext;
       // private readonly EmprestimoRepository _emprestimoRepository;

        public InsertProjectValidator(LivrosDbContext livrosDbContext)
        {

            _livrosDbContext = livrosDbContext;
            //_emprestimoRepository = emprestimoRepository;

            RuleFor(x => x.IdLivro)
                .GreaterThan(0)
                .WithMessage("#01. Livro Inválido");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0)
                .WithMessage("#02. Validation: Usuario Inválido");

            RuleFor(x => x.DataEmprestimo)
                .NotEmpty()
                .WithMessage("#03. Obrigatório incluir data do empréstimo");

            RuleFor(x => x.DataDevolucao)
                .NotEmpty()
                .WithMessage("#04. Obrigatório incluir data da devolução");

            RuleFor(x => x.DataDevolucao)
                .GreaterThan(x => x.DataEmprestimo) 
                .WithMessage("#05. Data da devolução deve ser posterior a data de empréstimo");

            RuleFor(x => x.DataDevolucao)
                .Must(date => date.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage("A devolução não pode ser num domingo.");
        }
    }
}
