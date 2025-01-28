using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models.Entidades;
using CrombieProytecto_V0._2.Service;
using System.Security.Cryptography;
using System.Text;

namespace CrombieProytecto_V0._2.Service
{
    public class UsuarioService
    {
        private readonly IAuthService _authService;
        private readonly ProyectContext _context;

        public UsuarioService(IAuthService authService, ProyectContext context)
        {
            _authService = authService;
            _context = context;
        }
        //Método para registrar nuevo usuario
        public async Task<Usuario> RegisterAsync(RegistroUsuarioDto registerDto)
        {
            return await _authService.RegisterUser(
                registerDto.Nombre,
                registerDto.Username,
                registerDto.Email,
                registerDto.Password);
        }
        //Método para iniciar sesión de usuario
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            return await _authService.Login(loginDto.Email, loginDto.Password);
        }
    }
}
