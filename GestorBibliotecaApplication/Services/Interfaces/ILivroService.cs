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

        ResultViewModel<List<LivroViewModel>> GetAll(string query);
        ResultViewModel<LivroDetailsModel> GetById(int id) ;
        ResultViewModel<int> Insert(NewLivroInputModel inputModel);
        ResultViewModel Update(UpdateLivroInputModel inputModel);
        ResultViewModel Delete(int id);
    }
}
