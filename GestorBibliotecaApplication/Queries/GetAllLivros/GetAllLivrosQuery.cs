using Dapper;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GestorBibliotecaApplication.Queries.GetAllLivros
{
    public class GetAllLivrosQuery : IRequest<ResultViewModel<List<LivroViewModel>>>
    {
        public string? Query { get; set; }
    }

    public class GetAllLivrosQueryHandler : IRequestHandler<GetAllLivrosQuery, ResultViewModel<List<LivroViewModel>>>
    {
       // private readonly LivrosDbContext _livrosDbContext;
        private readonly string _connString;

        public GetAllLivrosQueryHandler(LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
           // _livrosDbContext = livrosDbContext;
            _connString = configuration.GetConnectionString("GestorBibliotecaCs");
        }

        public async Task<ResultViewModel<List<LivroViewModel>>> Handle(GetAllLivrosQuery request, CancellationToken cancellationToken)
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

                    var livros =  ( await sqlConn.QueryAsync<LivroViewModel>(script, new { query = request.Query })).ToList();

                    return ResultViewModel<List<LivroViewModel>>.Success(livros);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro de SQL: {ex.Message}");
                return ResultViewModel<List<LivroViewModel>>.Error("Erro ao aceder a base de dados.");
            }
            catch (Exception ex)
            {
                return ResultViewModel<List<LivroViewModel>>.Error("Erro inesperado.");
            }
        }

    }
}
