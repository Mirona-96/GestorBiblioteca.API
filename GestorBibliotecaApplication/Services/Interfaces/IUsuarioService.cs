using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioViewModel GetUser(int id); //pesquisa usando o nome do usuario
        //UsuarioDetaislModel GetById(int id);
        public int Create(CreateUsuarioInputModel inputModel);
    }
}
