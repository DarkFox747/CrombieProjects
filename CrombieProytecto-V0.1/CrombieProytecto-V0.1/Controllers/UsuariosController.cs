using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrombieProytecto_V0._1.Service;
using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Context;
using CrombieProytecto_V0._1.Models.dtos;


namespace CrombieProytecto_V0._1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ProyectContext _context;

        public UsuariosController(IAuthService authService, ProyectContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("Registro")]
        public async Task<ActionResult<Usuario>> Register(RegistroUsuarioDto registerDto)
        {
            try
            {
                var usuario = await _authService.RegisterUser(
                    registerDto.Nombre,
                    registerDto.Username,
                    registerDto.Email,
                    registerDto.Password);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto.Email, loginDto.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
