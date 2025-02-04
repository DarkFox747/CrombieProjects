using Proyect_DAL.Context;
using Proyect_Models.Models.dtos;
using Proyect_BLL.Service;
using Proyect_Models.Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace Proyect_BLL.Service
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
