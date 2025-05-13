using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.DeleteLivro
{
    public class DeleteLivroCommand: IRequest<ResultViewModel>
    {
        public DeleteLivroCommand(int id)
        {
            Id = id;
        }
        
        public int Id { get; set; }
    }

}
