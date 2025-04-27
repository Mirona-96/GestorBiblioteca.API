namespace GestorBiblioteca.Core.Entities
{
    public class Usuario: BaseEntity
    {
        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
            DataCadastro = DateTime.Now;
            Emprestimos = new List<Emprestimo>();
            Activo = true;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public List<Emprestimo> Emprestimos { get; private set; }
        //um usuario pode realizar um ou varios emprestimos
        public bool Activo { get; private set; }
    }
}