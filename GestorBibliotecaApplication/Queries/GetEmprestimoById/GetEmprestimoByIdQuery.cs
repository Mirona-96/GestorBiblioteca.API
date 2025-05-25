using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Queries.GetEmprestimoById
{
    public class GetEmprestimoByIdQuery : IRequest<ResultViewModel<EmprestimoDetailsViewModel>>
    {
        public int Id { get; set; }

        public GetEmprestimoByIdQuery(int id)
        {
            Id = id;
        } 
    }

    public class GetEmprestimoByIdQueryHandler : IRequestHandler<GetEmprestimoByIdQuery, ResultViewModel<EmprestimoDetailsViewModel>>
    {
        private readonly LivrosDbContext _livrosDbContext;
        private readonly EmprestimoService _emprestimoService;

        public GetEmprestimoByIdQueryHandler(LivrosDbContext livrosDbContext, EmprestimoService emprestimoService)
        {
            _livrosDbContext = livrosDbContext;
            _emprestimoService = emprestimoService;
        }

        public async Task<ResultViewModel<EmprestimoDetailsViewModel>> Handle(GetEmprestimoByIdQuery request, CancellationToken cancellationToken)
        {
            var emprestimo = await _livrosDbContext.Emprestimos
                        .Include(u => u.Usuario)
                        .Include(l => l.Livro)
                        .SingleOrDefaultAsync(emp => emp.Id == request.Id);

            if (emprestimo == null)
            {
                return ResultViewModel<EmprestimoDetailsViewModel>.Error("Emprestimo inexistente");
            }

            var (nomeUsuario, tituloLivro) = _emprestimoService.BuscarNomeUsuarioTituloLivro(emprestimo.IdUsuario, emprestimo.IdLivro);

            var emprestimoDetailsViewModel = new EmprestimoDetailsViewModel
            {
                IdEmprestimo = emprestimo.Id,
                IdUsuario = emprestimo.IdUsuario,
                NomeUsuario = nomeUsuario,
                IdLivro = emprestimo.IdLivro,
                TituloLivro = tituloLivro,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao,
                Status = emprestimo.Status,
            };
            return ResultViewModel<EmprestimoDetailsViewModel>.Success(emprestimoDetailsViewModel);
        }
    }
}
