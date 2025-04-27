namespace GestorBibliotecaApplication.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string nome;
        public string email;

        public UsuarioViewModel(string nome, string email)
        {
            this.nome = nome;
            this.email = email;
        }
    }
}