using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LivrosDbContext _livrosDbContext;

        public UsuarioService(LivrosDbContext livrosDbContext)
        {
            _livrosDbContext = livrosDbContext;
        }

        public int Create(CreateUsuarioInputModel inputModel)
        {
            if (inputModel == null)
                throw new ArgumentNullException(nameof(inputModel));

            var usuario = new Usuario(inputModel.Nome, inputModel.Email);
            // _livrosDbContext.Usuarios ??= new List<Usuario>();
            _livrosDbContext.Usuarios.Add(usuario);
            return usuario.Id;
        }

        public UsuarioViewModel GetUser(int id)
        {
            var usuario = _livrosDbContext.Usuarios.SingleOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return null;
            }
            return new UsuarioViewModel(usuario.Nome, usuario.Email);
        }
    }
}
