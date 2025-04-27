using GestorBiblioteca.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Core.Entities
{
    public class Livro: BaseEntity
    {
        public Livro(string titulo, string autor, string isbn, int anoPublicacao)
        {
            Titulo = titulo;
            Autor = autor;
            Isbn = isbn;
            AnoPublicacao = anoPublicacao;
            Status = LivroStatusEnum.disponivel;
        }

        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Isbn { get; private set; }
        public int AnoPublicacao { get; private set; }
        public LivroStatusEnum Status { get; private set; }
        public int IdEmprestimo { get; private set; }
        public List<Emprestimo> emprestimos { get; private set; } = new();


        public void Update(string autor, string titulo, string isbn, int anoPublicacao)
        {
            Autor = autor;
            Titulo = titulo;
            Isbn = isbn;
            AnoPublicacao = anoPublicacao;
        }


        public void EliminarLivro(int id)
        {
            Status = LivroStatusEnum.indisponivel;
        }

        public void MarcarDisponivel()
            => Status = LivroStatusEnum.disponivel;

        public void MarcarIndisponivel()
            => Status = LivroStatusEnum.indisponivel;
    }
}
