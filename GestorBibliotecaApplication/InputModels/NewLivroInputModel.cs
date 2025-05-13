using GestorBiblioteca.Core.Entities;

namespace GestorBibliotecaApplication.InputModels
{
    public class NewLivroInputModel
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }

        public Livro ToEntity()
           => new(Titulo, Autor, ISBN, AnoPublicacao);
    }
}