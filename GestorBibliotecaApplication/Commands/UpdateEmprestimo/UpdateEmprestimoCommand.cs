using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.UpdateEmprestimo
{
    public class UpdateEmprestimoCommand: IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
