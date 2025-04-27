using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Interfaces
{
    public interface ILivroService
    {

        List<LivroViewModel> GetAll(string query);
        LivroDetailsModel GetById(int id);
        public int Create(NewLivroInputModel inputModel);
        void Update(UpdateLivroInputModel inputModel);
        void Delete(int id);
    }
}
