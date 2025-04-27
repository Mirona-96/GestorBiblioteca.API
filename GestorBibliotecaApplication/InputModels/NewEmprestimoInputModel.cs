namespace GestorBibliotecaApplication.InputModels
{
    public class NewEmprestimoInputModel
    {
        public int IdLivro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}