﻿using CrombieProytecto_V0._1.Context;
using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Models.dtos;
using CrombieProytecto_V0._1.Service;
using System.Security.Cryptography;
using System.Text;

namespace CrombieProytecto_V0._1.Service
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

        public async Task<Usuario> RegisterAsync(RegistroUsuarioDto registerDto)
        {
            return await _authService.RegisterUser(
                registerDto.Nombre,
                registerDto.Username,
                registerDto.Email,
                registerDto.Password);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            return await _authService.Login(loginDto.Email, loginDto.Password);
        }
    }
}
