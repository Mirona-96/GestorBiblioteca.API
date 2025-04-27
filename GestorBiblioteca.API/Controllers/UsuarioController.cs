using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestorBiblioteca.API.Controllers
{
    [Route("api/usuarios")]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUsuarioInputModel inputModel)
        {
            var id = _usuarioService.Create(inputModel);
            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuarioService.GetUser(id);

            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPut("{id}/login")]
        public IActionResult Login([FromBody] LoginUsuarioModel loginUsuarioModel)
        {
            // TODO: Para Módulo de Autenticação e Autorização
            return NoContent();
        }
    }
}
