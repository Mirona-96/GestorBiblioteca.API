using GestorBibliotecaApplication.Commands.CreateEmprestimo;
using GestorBibliotecaApplication.Commands.DeleteEmprestimo;
using GestorBibliotecaApplication.Commands.UpdateEmprestimo;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Queries.GetAllEmprestimos;
using GestorBibliotecaApplication.Queries.GetEmprestimoById;
using GestorBibliotecaApplication.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController: ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;
        private readonly IMediator _mediator;

       public EmprestimosController(IEmprestimoService emprestimoService, IMediator mediator)
        {
            _emprestimoService = emprestimoService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] InsertEmprestimoCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (command.DataDevolucao < command.DataEmprestimo)
                {
                    return BadRequest("Data invalida");
                }
                //var id = _emprestimoService.Insert(model);
                var result = await _mediator.Send(command);


                return CreatedAtAction(nameof(GetById), new { id = result.Data}, command);

            } 
            catch (Exception ex)
            {
                return UnprocessableEntity(new {Erro = ex.Message}); //captura erro emitido pelo metodo Insert()
            }
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            //buscar emprestimo
            //var emprestimo = _emprestimoService.GetById(id);
            var result = await _mediator.Send(new GetEmprestimoByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

           /* if (emprestimo == null)
                return NotFound();*/

            return Ok(result);
        }

        [HttpGet]
        public async Task <IActionResult> GetAll(string search = "")
        {
            //var result = _emprestimoService.GetAll(query);  
            var query = new GetAllEmprestimosQuery();

            var result = await _mediator.Send(query);

            //buscar todos os emprestimos com base numa pesquisa/busca
//            var emprestimos = _emprestimoService.GetAll(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task <IActionResult> Put(int id, UpdateEmprestimoCommand command)
        {
            if (command.DataDevolucao < command.DataEmprestimo)
            {
                return BadRequest("Data invalida");
            }

            var result = await _mediator.Send(command);

            //            _emprestimoService.Update(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

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

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete (int id)
        {
            var result = await _mediator.Send(new DeleteEmprestimoCommmand(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
