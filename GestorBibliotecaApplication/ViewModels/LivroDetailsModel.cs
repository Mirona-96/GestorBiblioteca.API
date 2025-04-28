using GestorBiblioteca.Core.Enums;

namespace GestorBibliotecaApplication.ViewModels
{
    public class LivroDetailsModel
    {
        public int IdLivro { get; internal set; }
        public string Titulo { get; internal set; }
        public string Autor { get; internal set; }
        public string ISBN { get; internal set; }
        public int AnoPublicacao { get; internal set; }
        public LivroStatusEnum Status { get; internal set; }
    }
}