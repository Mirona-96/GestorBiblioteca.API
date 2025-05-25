using GestorBiblioteca.Core.Entities;
using GestorBibliotecaApplication.Commands.CreateLivro;
using GestorBibliotecaApplication.Commands.DeleteLivro;
using GestorBibliotecaApplication.Commands.UpdateLivro;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Queries.GetAllLivros;
using GestorBibliotecaApplication.Queries.GetLivroById;
using GestorBibliotecaApplication.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorBiblioteca.API.Controllers
{
    [Route("api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly IMediator _mediator;

        public LivrosController (ILivroService livroService , IMediator mediator)
        {
            _livroService = livroService;
            _mediator = mediator;
        }

        //api/ivros?query
        [HttpGet]
        public async Task <IActionResult> GetAll (string search = "")
        {
            try
            {
                //buscar todos ou com filtro
                //var livros = _livroService.GetAll(query);
                var query = new GetAllLivrosQuery 
                { 
                    Query = search 
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return UnprocessableEntity(new { Erro = ex.Message }); //captura erro emitido pelo metodo GetAll()
            }

        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            //buscar o livro
            //var livro = _livroService.GetById(id);
            var result = await _mediator.Send(new GetLivroByIdQuery(id));

            /*if (livro == null)
                return NotFound("Livro não encontrado");*/

            if (!result.IsSuccess)
                return BadRequest(result.Message);
            //return NotFound
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertLivroCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Titulo))
            {
                return BadRequest("introduza o título do livro válido.");
            }
            if (string.IsNullOrEmpty(command.Autor))
            {
                return BadRequest("introduza o autor do livro");
            }
            if (command.AnoPublicacao > DateTime.Now.Year)
            {
                return BadRequest("Ano de Publicação inválido.");
            }

            //var id = _livroService.Insert(command);
            var result = await _mediator.Send(command);
            //cadastrar livro
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLivroCommand command)
        {
            if (string.IsNullOrEmpty(command.Titulo))
            {
                return BadRequest("introduza o título do livro válido.");
            }
            if (string.IsNullOrEmpty(command.Autor))
            {
                return BadRequest("introduza o autor do livro");
            }
            if (command.AnoPublicacao > DateTime.Now.Year)
            {
                return BadRequest("Ano de Publicação inválido.");
            }

            var result = await _mediator.Send (command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);
            //_livroService.Update(command);
            //Atualizar objecto

            return NotFound("Livro não encontrado");
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteLivroCommand(id));

            if (!result.IsSuccess)
                return NotFound(new { Mensagem = result.Message });

            return Ok(new { result.Message });

/*            try
            {
                _livroService.Delete(id);
                return NotFound("Livro não encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }*/
        }
    }
}
