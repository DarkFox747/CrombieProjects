using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CognitoAuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public CognitoAuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginDto request)
    {
        var token = await _authService.LoginWithCognito(request.Email, request.Password);
        return Ok(new { Token = token });
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegistroUsuarioDto registroUsuarioDto)
    {
        var userSub = await _authService.RegisterUserWithCognito(registroUsuarioDto);
        return Ok(new { UserSub = userSub });
    }

    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var token = await _authService.ChangePasswordWithCognito(request.Email, request.OldPassword, request.NewPassword);
        return Ok(new { Token = token });
    }
}


