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

namespace GestorBibliotecaApplication.Commands.DeleteEmprestimo
{
    public  class DeleteEmprestimoCommandHandler : IRequestHandler<DeleteEmprestimoCommmand, ResultViewModel>
    {
        private readonly LivrosDbContext _livrosDbContext;
       /// private readonly string _connString;

        public DeleteEmprestimoCommandHandler(LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
            _livrosDbContext = livrosDbContext;
            //_connString = configuration.GetConnectionString("GestorBibliotecaCs");
        }

        public async Task<ResultViewModel> Handle(DeleteEmprestimoCommmand request, CancellationToken cancellationToken)
        {
            var emprestimo = await _livrosDbContext.Emprestimos
                .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (emprestimo == null)
                return ResultViewModel.Error("Emprestimo inexistente");

            emprestimo.SetAsDeleted();
            _livrosDbContext.Emprestimos.Update(emprestimo);
            await _livrosDbContext.SaveChangesAsync(cancellationToken);

            return ResultViewModel.Sucess();
        }
    }
}
