namespace GestorBibliotecaApplication.InputModels
{
    public class UpdateEmprestimoInputModel
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}