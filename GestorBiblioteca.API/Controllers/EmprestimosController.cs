using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController: ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewEmprestimoInputModel inputModel)
        {
            if (inputModel.DataDevolucao < inputModel.DataEmprestimo)
            {
                return BadRequest("Data invalida");
            }
            var id = _emprestimoService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
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

        /*
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _emprestimoService.(id);
            return NoContent();
        }*/


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
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }   
    }
}
