using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.DeleteLivro
{
    public class DeleteLivroCommandHandler : IRequestHandler<DeleteLivroCommand, ResultViewModel>
    {
        private readonly LivrosDbContext _livrosDbContext;

        public DeleteLivroCommandHandler(LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
            _livrosDbContext = livrosDbContext;
        }

        async Task<ResultViewModel> IRequestHandler<DeleteLivroCommand, ResultViewModel>.Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await _livrosDbContext.Livros
                .SingleOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (livro == null)
                return ResultViewModel.Error("Livro inexistente");

            livro.SetAsDeleted();
            _livrosDbContext.Livros.Update(livro);
            await _livrosDbContext.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
