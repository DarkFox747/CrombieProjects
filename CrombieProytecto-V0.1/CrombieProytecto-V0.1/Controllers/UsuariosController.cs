﻿using Microsoft.AspNetCore.Mvc;
using CrombieProytecto_V0._1.Service;
using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Models.dtos;

namespace CrombieProytecto_V0._1.Controllers
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

        [HttpPost("Registro")]
        public async Task<ActionResult<Usuario>> Register(RegistroUsuarioDto registerDto)
        {
            try
            {
                var usuario = await _usuarioService.RegisterAsync(registerDto);
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
