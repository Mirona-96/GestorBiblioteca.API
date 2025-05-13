using GestorBiblioteca.Core.Entities;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.CreateLivro
{
    public class InsertLivroCommand: IRequest<ResultViewModel<int>>
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }

        public Livro ToEntity()
           => new(Titulo,Autor, ISBN, AnoPublicacao);
    }
}
