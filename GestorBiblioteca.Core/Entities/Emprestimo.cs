using GestorBiblioteca.Core.Enums;

namespace GestorBiblioteca.Core.Entities
{
    public class Emprestimo: BaseEntity
    {
        protected Emprestimo() { }

        public Emprestimo(int idUsuario, int idLivro, DateTime dataDevolucao)
            :base()
        {
            IdUsuario = idUsuario;
            IdLivro = idLivro;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = dataDevolucao;
            Status = EmprestimoStatusEnum.EmCurso;
        }

        public int IdLivro { get; private set; }
        public Livro Livro { get; private set; } 

        public int IdUsuario { get; private set; }
        public Usuario Usuario { get; private set; }

        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public EmprestimoStatusEnum Status { get; private set; }


/*        public void Atrasado(DateTime data)
        {
            if ((Status == EmprestimoStatusEnum.Criado || Status == EmprestimoStatusEnum.EmCurso) && (data > DataDevolucao))
            {
                int diasAtraso = (data - DataDevolucao).Days;
                Status = EmprestimoStatusEnum.Atrasado;
            }
            else
            {
                Status = EmprestimoStatusEnum.Terminado;
            }
        }*/

        /*public void TerminarEmprestimo()
        {
            if (Status == EmprestimoStatusEnum.Criado || Status == EmprestimoStatusEnum.EmCurso)
            {
                Status = EmprestimoStatusEnum.Cancelado;
            }
        }*/

       /* public void Criado()
        {
            if (Status == EmprestimoStatusEnum.Criado)
            {
                Status = EmprestimoStatusEnum.EmCurso;
                DataEmprestimo = DateTime.Now;
            }
        }*/

        public int RegistarDevolucao(DateTime dataEntrega, Livro livro)
        {
            if ((Status == EmprestimoStatusEnum.Criado) || (Status == EmprestimoStatusEnum.EmCurso))
            {
                if (dataEntrega > DataDevolucao)
                {
                    Status = EmprestimoStatusEnum.Atrasado;
                    livro.MarcarDisponivel();
                    return (dataEntrega - DataDevolucao).Days;
                }
                else
                {
                    Status = EmprestimoStatusEnum.Terminado;
                    livro.MarcarDisponivel();
                    return 0;
                }
            }
            throw new InvalidOperationException("Empréstimo finalizado.");
        }

        public void Update(DateTime dataDevolucao)
        {
            if ((Status == EmprestimoStatusEnum.Criado) || (Status == EmprestimoStatusEnum.EmCurso))
            {
                DataDevolucao = dataDevolucao;
            }
        }
    }
}
