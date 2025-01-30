using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint para registro local (JWT)
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistroUsuarioDto registroUsuarioDto)
        {
            try
            {
                var usuario = await _authService.RegisterUser(registroUsuarioDto.Nombre, registroUsuarioDto.Username, registroUsuarioDto.Email, registroUsuarioDto.Password);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para inicio de sesión local (JWT)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto.Email, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Endpoint para registro con Cognito
        [HttpPost("register-cognito")]
        public async Task<IActionResult> RegisterWithCognito(RegistroUsuarioDto registroUsuarioDto)
        {
            try
            {
                var userSub = await _authService.RegisterUserWithCognito(registroUsuarioDto);
                return Ok(new { UserSub = userSub });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para inicio de sesión con Cognito
        [HttpPost("login-cognito")]
        public async Task<IActionResult> LoginWithCognito(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginWithCognito(loginDto.Email, loginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            try
            {
                var result = await _authService.ConfirmEmailAsync(confirmEmailDto.Email, confirmEmailDto.ConfirmationCode);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var result = await _authService.ChangePasswordAsync(changePasswordDto.Email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
