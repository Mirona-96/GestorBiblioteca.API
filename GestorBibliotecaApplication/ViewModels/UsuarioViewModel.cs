using GestorBiblioteca.Core.Entities;

namespace GestorBibliotecaApplication.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(string nome, string email, DateTime dataCadastro, List<Emprestimo> emprestimos)
        {
            this.nome = nome;
            this.email = email;
            DataCadastro = dataCadastro;
            Emprestimos = emprestimos;
        }

        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }

        public static UsuarioViewModel FromEntity(Usuario usuario)
        {
            var emprestimos = usuario.Emprestimos.Select(u => u.Livro.Titulo).ToList();

            return new UsuarioViewModel(usuario.Nome, usuario.Email, usuario.DataCadastro,usuario.Emprestimos);
        }

        /*   public UsuarioViewModel(string nome, string email, DateTime dataCadastro, List<Emprestimo> emprestimos)
           {
               this.nome = nome;
               this.email = email;
               DataCadastro = dataCadastro;
               Emprestimos = emprestimos;
           }*/
    }
}