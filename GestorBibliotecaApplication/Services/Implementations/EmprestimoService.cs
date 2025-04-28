using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Core.Enums;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Implementations
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly LivrosDbContext _livrosDbContext;

        public EmprestimoService(LivrosDbContext livrosDbContext)
        {
            _livrosDbContext = livrosDbContext;
        }

        private (string NomeUsuario, string TituloLivro) BuscarNomeUsuarioTituloLivro(int idUsuario, int idLivro)
        {

            //realiza consultas isoladas
            var usuario = _livrosDbContext.Usuarios
                .AsNoTracking() // não manter no tracking (só leitura)
                .FirstOrDefault(u => u.Id == idUsuario);

            var livro = _livrosDbContext.Livros
                .AsNoTracking()
                .FirstOrDefault(l => l.Id == idLivro);

            string NomeUsuario = usuario != null ? usuario.Nome : "Usuario não encontrado";
            string TituloLivro = livro != null ? livro.Titulo : "Livro não encontrado";

            return (NomeUsuario, TituloLivro);
        }

        /*  public void Atrasado(int id)
          {
              var emprestimo = _livrosDbContext.Emprestimos.SingleOrDefault(emp => emp.Id == id);

              DateTime data = DateTime.Now;
              emprestimo.Atrasado(data);
          }*/

        public int Create(NewEmprestimoInputModel inputModel)
        {
            var livro = _livrosDbContext.Livros.FirstOrDefault(l => l.Id == inputModel.IdLivro);
            if (livro == null)
                throw new Exception("livro não encontrado.");
            if (livro.Status == LivroStatusEnum.indisponivel)
                throw new Exception("Livro indisponivel");

            var emprestimo = new Emprestimo(inputModel.IdUsuario, inputModel.IdLivro, inputModel.DataDevolucao);

            livro.MarcarIndisponivel();
            _livrosDbContext.Emprestimos.Add(emprestimo);
            _livrosDbContext.SaveChanges();
            return emprestimo.Id;
        }

     /*   public void Criado(int id)
        {
            var emprestimo = _livrosDbContext.Emprestimos.SingleOrDefault(emp => emp.Id == id);

            emprestimo.Criado();
        }*/

      /*  public void Delete(int id)
        {
            var emprestimo = _livrosDbContext.Emprestimos.SingleOrDefault(e => e.Id == id);
            emprestimo.TerminarEmprestimo();
        }*/

        public int DevolverLivro(int id, DateTime data)
        {
            var emprestimo = _livrosDbContext.Emprestimos.SingleOrDefault(emp => emp.Id == id);

            if (emprestimo == null)
                throw new Exception("Emprestimo não encontrado");

            var dataEntrega = data != default ? data : DateTime.Now;
            var livro = _livrosDbContext.Livros.SingleOrDefault(emp => emp.Id == id);
            _livrosDbContext.SaveChanges();
            return emprestimo.RegistarDevolucao(dataEntrega, livro);

        }

        public List<EmprestimoViewModel> GetAll(string query)
        {
            var emprestimoViewModel = new List<EmprestimoViewModel>();

            foreach (var emprestimo in _livrosDbContext.Emprestimos)
            {
                var (nomeUsuario, tituloLivro) = BuscarNomeUsuarioTituloLivro(emprestimo.IdUsuario, emprestimo.IdLivro);

                if (!string.IsNullOrWhiteSpace(query) &&
                        !nomeUsuario.Contains(query, StringComparison.OrdinalIgnoreCase) &&
                        !tituloLivro.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    continue; //filtra se encontrar a "query"
                }

                emprestimoViewModel.Add(new EmprestimoViewModel
                {
                    IdEmprestimo = emprestimo.Id,
                    NomeUsuario = nomeUsuario,
                    TituloLivro = tituloLivro,
                    DataEmprestimo = emprestimo.DataEmprestimo
                });
            }
            _livrosDbContext.SaveChanges();
            return emprestimoViewModel;
        }

        public EmprestimoDetailsViewModel GetById(int id)
        {


            var emprestimo = _livrosDbContext.Emprestimos
                .Include(u => u.Usuario)
                .Include(l => l.Livro)
                .SingleOrDefault(emp => emp.Id == id);

            if (emprestimo == null) return null;

            var (nomeUsuario, tituloLivro) = BuscarNomeUsuarioTituloLivro(emprestimo.IdUsuario, emprestimo.IdLivro);

            var emprestimoDetailsViewModel = new EmprestimoDetailsViewModel
            {
                IdEmprestimo = emprestimo.Id,
                IdUsuario = emprestimo.IdUsuario,
                NomeUsuario = nomeUsuario,
                IdLivro = emprestimo.IdLivro,
                TituloLivro = tituloLivro,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao,
                Status = emprestimo.Status,
            };
            _livrosDbContext.SaveChanges();
            return emprestimoDetailsViewModel;
        }

       /* public void Terminado(int id)
        {
            throw new NotImplementedException();
        }*/

        public void Update(UpdateEmprestimoInputModel inputModel)
        {
            var emprestimo = _livrosDbContext.Emprestimos.SingleOrDefault(emp => emp.Id == inputModel.Id);
            if (emprestimo == null)
                throw new Exception("Emprestimo não encontrado");

            emprestimo.Update(inputModel.DataDevolucao);
            _livrosDbContext.SaveChanges();
        }
    }
}
