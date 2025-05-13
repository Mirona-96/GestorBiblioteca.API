using GestorBiblioteca.Core.Entities;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorBiblioteca.API.Controllers
{
    [Route("api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController (ILivroService livroService)
        {
            _livroService = livroService;
        }

        //api/ivros?query
        [HttpGet]
        public IActionResult Get (string query)
        {
            try
            {
                //buscar todos ou com filtro
                var livros = _livroService.GetAll(query);
                return Ok(livros);
            } 
            catch (Exception ex)
            {
                return UnprocessableEntity(new { Erro = ex.Message }); //captura erro emitido pelo metodo GetAll()
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //buscar o livro
            var livro = _livroService.GetById(id);
            if (livro == null)
                return NotFound("Livro não encontrado");
            //return NotFound
            return Ok(livro);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewLivroInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.Titulo))
            {
                return BadRequest("introduza o título do livro válido.");
            }
            if (string.IsNullOrEmpty(inputModel.Autor))
            {
                return BadRequest("introduza o autor do livro");
            }
            if (inputModel.AnoPublicacao > DateTime.Now.Year)
            {
                return BadRequest("Ano de Publicação inválido.");
            }

            var id = _livroService.Insert(inputModel);
            //cadastrar livro
            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        [HttpPut ("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateLivroInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Titulo))
            {
                return BadRequest("introduza o título do livro válido.");
            }
            if (string.IsNullOrEmpty(inputModel.Autor))
            {
                return BadRequest("introduza o autor do livro");
            }
            if (inputModel.AnoPublicacao > DateTime.Now.Year)
            {
                return BadRequest("Ano de Publicação inválido.");
            }
            _livroService.Update(inputModel);
            //Atualizar objecto

            return NotFound("Livro não encontrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _livroService.Delete(id);
                return NotFound("Livro não encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }
    }
}
