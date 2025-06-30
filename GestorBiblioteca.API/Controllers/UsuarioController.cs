using GestorBiblioteca.Core.Entities;
using GestorBiblioteca.Infrastructure.Auth;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.InputModels;
using GestorBibliotecaApplication.Services.Interfaces;
using GestorBibliotecaApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioController: ControllerBase
    {
       // private readonly IUsuarioService _usuarioService;
        private readonly LivrosDbContext _livroDbContext;
        private readonly IAuthService _authService;

        public UsuarioController(LivrosDbContext livrosDbContext, IAuthService authService)
        {
         //   _usuarioService = usuarioService;
            _livroDbContext = livrosDbContext;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CreateUsuarioInputModel model)
        {
            var hash= _authService.ComputeHash(model.Password);
            var usuario = new Usuario(model.Nome, model.Email, hash, model.Role);

            _livroDbContext.Add(usuario);
            _livroDbContext.SaveChanges();
            return NoContent();
         //   var id = _usuarioService.Create(inputModel);
           // return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           // var usuario = _usuarioService.GetUser(id);

            var usuario = _livroDbContext.Usuarios
                .Include(u => u.Emprestimos)
                .ThenInclude(u => u.DataEmprestimo)
                .SingleOrDefault(u => u.Id == id);

            if (usuario == null)
                return NotFound("usuario nao encontrado");

            var model = UsuarioViewModel.FromEntity(usuario);
            return Ok(model);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel model)
        {
            var hash = _authService.ComputeHash(model.Password);

            var user = _livroDbContext.Usuarios.SingleOrDefault(u => u.Email == model.Email && u.Password == hash);

            if (user is null)
            {
                var error = ResultViewModel<LoginViewModel?>.Error("Erro de login.");

                 return BadRequest(error);
            }

            var token = _authService.GenerateToken(user.Email, user.Role);

            var viewModel = new LoginViewModel(token);

            var result = ResultViewModel<LoginViewModel?>.Success(viewModel);

            return Ok(result);

/*            // TODO: Para Módulo de Autenticação e Autorização
            return NoContent();*/
        }
    }
}
