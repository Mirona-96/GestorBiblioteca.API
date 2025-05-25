using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.CreateEmprestimo
{
    public class ValidateInsertProjectCommandBehavior :
                IPipelineBehavior<InsertEmprestimoCommand, ResultViewModel<int>>
    {

        private readonly LivrosDbContext _livroDbContext;

        public ValidateInsertProjectCommandBehavior(LivrosDbContext livroDbContext)
        {
            _livroDbContext = livroDbContext;
        }

        public async Task<ResultViewModel<int>> Handle(InsertEmprestimoCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var usuarioExists = _livroDbContext.Usuarios.Any(u => u.Id == request.IdUsuario);
            var livroExists = _livroDbContext.Livros.Any(l=> l.Id == request.IdLivro);

            if (!usuarioExists || !livroExists)
                return ResultViewModel<int>.Error("Usuario ou Livro invalidos");



            return await next();
        }
    }
}
