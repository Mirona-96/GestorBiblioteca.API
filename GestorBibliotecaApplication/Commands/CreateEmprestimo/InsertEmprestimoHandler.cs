using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Core.Enums;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.Notification;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.CreateEmprestimo
{
    public class InsertEmprestimoHandler : IRequestHandler<InsertEmprestimoCommand, ResultViewModel<int>>
    {
        private readonly LivrosDbContext _livrosDbContext;
        private readonly IMediator _mediator;
       
        public InsertEmprestimoHandler(LivrosDbContext livrosDbContext, IConfiguration configuration, IMediator mediator)
        {
            _livrosDbContext = livrosDbContext;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertEmprestimoCommand request, CancellationToken cancellationToken)
        {
            var livro = await _livrosDbContext.Livros
                .FirstOrDefaultAsync(l => l.Id == request.IdLivro,cancellationToken);

            if (livro == null)
                return ResultViewModel<int>.Error("Livro não encontrado.");

            if (livro.Status == LivroStatusEnum.indisponivel)
                return ResultViewModel<int>.Error("Livro indisponível.");


            //var emprestimo = new Emprestimo(inputModel.IdUsuario, inputModel.IdLivro, inputModel.DataDevolucao);
            var emprestimo = request.ToEntity();

            livro.MarcarIndisponivel();

            await _livrosDbContext.Emprestimos.AddAsync(emprestimo);
            await _livrosDbContext.SaveChangesAsync();
            await _mediator.Publish(new EmprestimoCreatedNotification(emprestimo.Id), cancellationToken);
            return ResultViewModel<int>.Success(emprestimo.Id);
        }
    }
}
