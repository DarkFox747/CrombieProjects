using Microsoft.AspNetCore.Mvc;
using CrombieProytecto_V0._2.Service;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models.Entidades;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        //Registra un nuevo Usuario
        [HttpPost("Registro")]
        public async Task<ActionResult<Usuario>> Register([FromQuery] string nombre, [FromQuery] string username, [FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var registerDto = new RegistroUsuarioDto
                {
                    Nombre = nombre,
                    Username = username,
                    Email = email,
                    Password = password
                };

                var usuario = await _usuarioService.RegisterAsync(registerDto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Permite iniciar sesión a un usuario registrado
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var loginDto = new LoginDto
                {
                    Email = email,
                    Password = password
                };

                var token = await _usuarioService.LoginAsync(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
