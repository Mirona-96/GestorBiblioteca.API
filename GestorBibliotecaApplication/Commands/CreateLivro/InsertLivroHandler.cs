using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Core.Enums;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.CreateLivro
{
    public class InsertLivroHandler : IRequestHandler<InsertLivroCommand, ResultViewModel<int>>
    {
        private readonly LivrosDbContext _livrosDbContext;

        public InsertLivroHandler(LivrosDbContext livrosDbContext, IConfiguration configuration)
        {
            _livrosDbContext = livrosDbContext;
        }

        public async Task<ResultViewModel<int>> Handle(InsertLivroCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var livro = new Livro(request.Titulo, request.Autor, request.ISBN, request.AnoPublicacao);

            await _livrosDbContext.Livros.AddAsync(livro);
            await _livrosDbContext.SaveChangesAsync();
            return ResultViewModel<int>.Success(livro.Id);
        }
    }
}
