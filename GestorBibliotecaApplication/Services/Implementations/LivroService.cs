using Dapper;
using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Implementations
{
    public class LivroService : ILivroService
    {
        private readonly LivrosDbContext _livrosDbContext;
        private readonly string _connString;
        

        public LivroService (LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
            _livrosDbContext = livrosDbContext;
            _connString = configuration.GetConnectionString("GestorBibliotecaCs");
        }

        public int Create(NewLivroInputModel inputModel)
        {
            if (inputModel == null)
                throw new ArgumentNullException(nameof(inputModel));

            var livro = new Livro(inputModel.Titulo, inputModel.Autor, inputModel.ISBN, inputModel.AnoPublicacao);

            _livrosDbContext.Livros.Add(livro);
            _livrosDbContext.SaveChanges();
            return livro.Id;
        }

        public void Delete(int id)
        {
            var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == id);
            livro.EliminarLivro(id);
            _livrosDbContext.SaveChanges();
        }

        public List<LivroViewModel> GetAll(string query)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_connString))
                {
                    sqlConn.Open();
                    var script = @"
                SELECT Id, Titulo, Autor
                FROM Livros
                WHERE (@query IS NULL OR
                       LOWER(Titulo) LIKE '%' + LOWER(@query) + '%' OR
                       LOWER(Autor) LIKE '%' + LOWER(@query) + '%')";

                    var livros = sqlConn.Query<LivroViewModel>(script, new { query }).ToList();

                    return livros;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro de SQL: {ex.Message}");
                throw new Exception("Erro ao aceder a base de dados.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ocorrido: {ex.Message}");
                throw;
            }


           /* var livrosQuery = _livrosDbContext.Livros.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {

                livrosQuery = livrosQuery
                    .Where(l =>
                    l.Titulo.ToLower().Contains(query.ToLower()) ||
                    l.Autor.ToLower().Contains(query.ToLower()));
            }

            var livrosViewModel = livrosQuery
            .Select(l => new LivroViewModel(l.Id, l.Titulo, l.Autor))
            .ToList();
         //   _livrosDbContext.SaveChanges();
            return livrosViewModel;*/
        }

        public LivroDetailsModel GetById(int id)
        {
            var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == id);

            if (livro == null) return null;

            var livroDetailsModel = new LivroDetailsModel
            {
                IdLivro = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.Isbn,
                AnoPublicacao = livro.AnoPublicacao,
                Status = livro.Status
            };
            return livroDetailsModel;
        }

        public void Update(UpdateLivroInputModel inputModel)
        {
            var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == inputModel.Id);
            livro.Update(inputModel.Autor, inputModel.Titulo, inputModel.ISBN, inputModel.AnoPublicacao);
            _livrosDbContext.SaveChanges();
        }
    }
}
