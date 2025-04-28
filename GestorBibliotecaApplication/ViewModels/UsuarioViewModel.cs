namespace GestorBibliotecaApplication.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public UsuarioViewModel(string nome, string email)
        {
            this.nome = nome;
            this.email = email;
        }
    }
}