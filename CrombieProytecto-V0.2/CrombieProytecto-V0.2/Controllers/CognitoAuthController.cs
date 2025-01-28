using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CognitoAuthController : ControllerBase
{
    private readonly ICognitoAuthService _authService;

    public CognitoAuthController(ICognitoAuthService authService)
    {
        _authService = authService;
    }
    //Inicia sesión a un usuario
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginDto request)
    {
        var token = await _authService.LoginAsync(request.Email, request.Password);
        return Ok(new { Token = token });
    }
    //Registra un nuevo usuario
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegistroUsuarioDto registroUsuarioDto)
    {
        var userSub = await _authService.RegisterUserAsync(registroUsuarioDto);
        return Ok(new { UserSub = userSub });
    }
    //Actualiza la password de un usuario
    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var token = await _authService.ChangePasswordAsync(request.Email, request.OldPassword, request.NewPassword);
        return Ok(new { Token = token });
    }
}


