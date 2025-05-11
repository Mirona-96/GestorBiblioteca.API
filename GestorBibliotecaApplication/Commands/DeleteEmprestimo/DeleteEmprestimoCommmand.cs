using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.DeleteEmprestimo
{
    public class DeleteEmprestimoCommmand: IRequest<ResultViewModel>
    {
        public DeleteEmprestimoCommmand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
