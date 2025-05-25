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

namespace GestorBibliotecaApplication.Commands.UpdateEmprestimo
{
    public class UpdateEmprestimoCommandHandler : IRequestHandler<UpdateEmprestimoCommand, ResultViewModel>
    {
        private readonly LivrosDbContext _livrosDbContext;

        public UpdateEmprestimoCommandHandler(LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
            _livrosDbContext = livrosDbContext;
        }

        public async Task<ResultViewModel> Handle(UpdateEmprestimoCommand request, CancellationToken cancellationToken)
        {
            var emprestimo = await _livrosDbContext.Emprestimos.SingleOrDefaultAsync(emp => emp.Id == request.Id);
            if (emprestimo == null)
                return ResultViewModel.Error("Emprestimo inexistente");

            emprestimo.Update(request.DataDevolucao);
            _livrosDbContext.Emprestimos.Update(emprestimo);
            await _livrosDbContext.SaveChangesAsync();
            return ResultViewModel.Sucess();
        }
    }
}
