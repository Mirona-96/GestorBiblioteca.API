using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.ViewModels;
//using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GestorBibliotecaApplication.Queries.GetAllEmprestimos
{
    public class GetAllEmprestimosQuery: IRequest<ResultViewModel<List<EmprestimoViewModel>>>
    {
        public string? Query {  get; set; }
    }

    public class GetAllEmprestimosHandler : IRequestHandler<GetAllEmprestimosQuery, ResultViewModel<List<EmprestimoViewModel>>>
    {

        private readonly LivrosDbContext _livrosDbContext;
        private readonly EmprestimoService _emprestimoService;

        public GetAllEmprestimosHandler(LivrosDbContext livrosDbContext, EmprestimoService emprestimoService)
        {
            _livrosDbContext = livrosDbContext;
            _emprestimoService = emprestimoService;
        }

        public async Task<ResultViewModel<List<EmprestimoViewModel>>> Handle(GetAllEmprestimosQuery request, CancellationToken cancellationToken)
        {
            var emprestimos = await _livrosDbContext.Emprestimos.ToListAsync(cancellationToken);
            var emprestimoViewModel = new List<EmprestimoViewModel>();

            foreach (var emprestimo in emprestimos)
            {
                var (nomeUsuario, tituloLivro) = _emprestimoService.BuscarNomeUsuarioTituloLivro(emprestimo.IdUsuario, emprestimo.IdLivro);
                if (!string.IsNullOrWhiteSpace(request.Query) &&
                    !(nomeUsuario.Contains(request.Query, StringComparison.OrdinalIgnoreCase) ||
                      tituloLivro.Contains(request.Query, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                emprestimoViewModel.Add(new EmprestimoViewModel
                {
                    IdEmprestimo = emprestimo.Id,
                    NomeUsuario = nomeUsuario,
                    TituloLivro = tituloLivro,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao
                });
            }
            return ResultViewModel<List<EmprestimoViewModel>>.Success(emprestimoViewModel);
        }
    }
}
