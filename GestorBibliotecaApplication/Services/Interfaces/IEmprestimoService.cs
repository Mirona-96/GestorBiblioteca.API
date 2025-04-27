using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Services.Interfaces
{
    public interface IEmprestimoService
    {
        List<EmprestimoViewModel> GetAll(string query);
        EmprestimoDetailsViewModel GetById(int id);
        int Create(NewEmprestimoInputModel inputModel);
        void Update(UpdateEmprestimoInputModel inputModel);
     //   void Delete(int id);
       // void Criado(int id);
       // void Atrasado(int id);
      //  void Terminado(int id);
        int DevolverLivro(int id, DateTime data); //retorna eventuais dias de atraso
    }
}
