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
        ResultViewModel <List<EmprestimoViewModel>> GetAll(string query);
        List <EmprestimoViewModel> GetEmprestimoUsuario(int idUsuario);
        ResultViewModel <EmprestimoDetailsViewModel> GetById(int id);
        ResultViewModel<int> Insert(InsertEmprestimoInputModel inputModel);
        ResultViewModel Update(UpdateEmprestimoInputModel inputModel);
        ResultViewModel Delete(int id);
        // void Criado(int id);
        // void Atrasado(int id);
        //  void Terminado(int id);
        int DevolverLivro(int id, DateTime data); //retorna eventuais dias de atraso
        
    }
}
