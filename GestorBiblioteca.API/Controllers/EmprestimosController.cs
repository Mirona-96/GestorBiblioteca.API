using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController: ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

       public EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewEmprestimoInputModel inputModel)
        {
            try
            {
                if (inputModel.DataDevolucao < inputModel.DataEmprestimo)
                {
                    return BadRequest("Data invalida");
                }
                var id = _emprestimoService.Create(inputModel);

                return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);

            } 
            catch (Exception ex)
            {
                return UnprocessableEntity(new {Erro = ex.Message}); //captura erro emitido pelo metodo Create()
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //buscar emprestimo
            var emprestimo = _emprestimoService.GetById(id);

            if (emprestimo == null)
                return NotFound();

            return Ok(emprestimo);
        }

        [HttpGet]
        public IActionResult GetAll(string query)
        {
            //buscar todos os emprestimos com base numa pesquisa/busca
            var emprestimos = _emprestimoService.GetAll(query);
            return Ok(emprestimos);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateEmprestimoInputModel inputModel)
        {
            if (inputModel.DataDevolucao < inputModel.DataEmprestimo)
            {
                return BadRequest("Data invalida");
            }

            _emprestimoService.Update(inputModel);

            return NoContent();
        }

        [HttpPut("{id}/devolucao")]
        public IActionResult DevolverLivro (int id, [FromBody] DateTime dataEntrega)
        {
            try
            {
                int diasAtraso = _emprestimoService.DevolverLivro(id, dataEntrega);
                return Ok(new
                {
                    Mensagem = diasAtraso > 0
                    ? $"Livro devolvido com {diasAtraso} dia(s) de atraso."
                    : "Livro devoldido dentro do prazo.",
                    DiasAtraso = diasAtraso
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });  //captura erro ao n encontrar o id do Emprestimo
            } 
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { Erro = ex.Message }); //captura erro emitido pelo metodo Devolver()
            }
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult GetEmprestimosUsuario(int idUsuario)
        {
            try
            {
                var emprestimos = _emprestimoService.GetEmprestimoUsuario(idUsuario);

                if (emprestimos == null)
                    return NotFound(new {Mensagem = "nenhum emprestimo realizado pelo usuario." });

                return Ok(emprestimos);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
