using GestorBiblioteca.Core.Entities;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.CreateEmprestimo
{
    public class InsertEmprestimoCommand: IRequest<ResultViewModel<int>>
    {
        public int IdLivro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        public Emprestimo ToEntity()
        => new(IdLivro, IdUsuario, DataDevolucao);
    }
}
