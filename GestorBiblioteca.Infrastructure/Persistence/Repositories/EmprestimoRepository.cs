using Azure.Core;
using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Persistence.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly LivrosDbContext _livrosDbContext;
        public EmprestimoRepository(LivrosDbContext livrosDbContext)
        {
            _livrosDbContext = livrosDbContext;
        }
        public async Task <int> Add(Emprestimo emprestimo)
        {
            await _livrosDbContext.Emprestimos.AddAsync(emprestimo);
            await _livrosDbContext.SaveChangesAsync();
          //  await _mediator.Publish(new EmprestimoCreatedNotification(emprestimo.Id), cancellationToken);
            //return ResultViewModel<int>.Success(emprestimo.Id);

            return emprestimo.Id;
        }

        async Task<bool> IEmprestimoRepository.Exists(int id)
        {
            return await _livrosDbContext.Emprestimos.AnyAsync(e => e.Id == id);
        }

        public async Task<List<Emprestimo>> GetAll()
        {
            var emprestimos = await _livrosDbContext.Emprestimos
                    .Where(e => !e.IsDeleted)
                    .ToListAsync();

            return emprestimos;
        }

        public async Task<Emprestimo?> GetById(int id)
        {

            return await _livrosDbContext.Emprestimos
                .SingleOrDefaultAsync(emp => emp.Id == id && !emp.IsDeleted);
        }

        public async Task<Emprestimo?> GetDetailsById(int id)
        {
            var emprestimo = await _livrosDbContext.Emprestimos
                        .Include(u => u.Usuario)
                        .Include(l => l.Livro)
                        .SingleOrDefaultAsync(emp => emp.Id == id && !emp.IsDeleted);

            return emprestimo;

        }

        public async Task Update(Emprestimo emprestimo)
        {
            _livrosDbContext.Emprestimos.Update(emprestimo);
            await _livrosDbContext.SaveChangesAsync();
        }
    }
}
