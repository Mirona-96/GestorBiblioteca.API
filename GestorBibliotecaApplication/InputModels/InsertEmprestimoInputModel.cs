using GestorBiblioteca.Core.Entities;

namespace GestorBibliotecaApplication.InputModels
{
    public class InsertEmprestimoInputModel
    {
        public int IdLivro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        public Emprestimo ToEntity()
            => new(IdLivro, IdUsuario, DataDevolucao);
    }
}