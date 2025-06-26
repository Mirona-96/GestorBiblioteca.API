using GestorBiblioteca.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Core.Repositories
{
    public interface IEmprestimoRepository
    {
        Task<List<Emprestimo>> GetAll();
        Task<Emprestimo?> GetById(int id);
        Task<Emprestimo?> GetDetailsById(int id);
        Task<int> Add(Emprestimo emprestimo);
        Task Update(Emprestimo emprestimo);
        Task<bool> Exists(int id); 
    }
}
