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

        public ResultViewModel<int> Insert (NewLivroInputModel inputModel)
        {
            if (inputModel == null)
                throw new ArgumentNullException(nameof(inputModel));

            var livro = inputModel.ToEntity();

            _livrosDbContext.Livros.Add(livro);
            _livrosDbContext.SaveChanges();
            return ResultViewModel<int>.Success (livro.Id);
        }

        public ResultViewModel Delete(int id)
        {        
          var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == id);
          if (livro == null)
          return ResultViewModel.Error("Emprestimo inexistente");


          livro.EliminarLivro(id);
          livro.SetAsDeleted();
          _livrosDbContext.Livros.Update(livro);
          _livrosDbContext.SaveChanges();

          return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<LivroViewModel>>  GetAll(string query)
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

                    return ResultViewModel<List<LivroViewModel>>.Success (livros);
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

        public ResultViewModel <LivroDetailsModel> GetById(int id)
        {
            var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == id);


            if (livro == null)
                return ResultViewModel<LivroDetailsModel>.Error("Livro inexistente");

            var livroDetailsModel = new LivroDetailsModel
            {
                IdLivro = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.Isbn,
                AnoPublicacao = livro.AnoPublicacao,
                Status = livro.Status
            };
            return ResultViewModel <LivroDetailsModel>.Success(livroDetailsModel);
        }

        public ResultViewModel Update(UpdateLivroInputModel inputModel)
        {
            var livro = _livrosDbContext.Livros.SingleOrDefault(l => l.Id == inputModel.Id);

           if (livro == null)
                 return ResultViewModel.Error("Livro inexistente");

            livro.Update(inputModel.Autor, inputModel.Titulo, inputModel.ISBN, inputModel.AnoPublicacao);
            _livrosDbContext.SaveChanges();
            return ResultViewModel.Sucess();
        }
    }
}
