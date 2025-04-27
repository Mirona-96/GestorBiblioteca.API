
using GestorBiblioteca.Core.Enums;

namespace GestorBibliotecaApplication.ViewModels
{
    public class EmprestimoDetailsViewModel
    {
        public int IdEmprestimo { get; set; }
        public int IdLivro { get; set; }
        public string TituloLivro { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public EmprestimoStatusEnum Status { get; set; }
    }
}