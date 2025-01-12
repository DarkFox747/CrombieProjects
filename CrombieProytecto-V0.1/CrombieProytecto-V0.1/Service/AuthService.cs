using CrombieProytecto_V0._1.Context;
using CrombieProytecto_V0._1.Models;
using Humanizer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CrombieProytecto_V0._1.Service
{
    public interface IAuthService
    {
        Task<Usuario> RegisterUser(string nombre, string username, string email, string password);
        Task<string> Login(string email, string password);
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string hash, string salt);
    }

    public class AuthService : IAuthService
    {
        private readonly ProyectContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ProyectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Usuario> RegisterUser(string Nombre,string username, string email, string password)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                throw new Exception("Email already exists");

            string salt;
            string hashedPassword = HashPassword(password, out salt);

            var Usuario = new Usuario
            {
                Nombre = Nombre,
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt,
                Roles = UserRole.Regular,
                CreatedAt = DateTime.UtcNow
            };

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();
            return Usuario;
        }

        public async Task<string> Login(string email, string password)
        {
            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (Usuario == null)
                throw new Exception("Credendciales Invalidas");

            if (!VerifyPassword(password, Usuario.PasswordHash, Usuario.Salt))
                throw new Exception("Credendciales Invalidas");

            // Generar JWT Token
            var token = GenerateJwtToken(Usuario);
            return token;
        }

        public string HashPassword(string password, out string salt)
        {
            salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8));
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            string newHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return newHash == hash;
        }

        private string GenerateJwtToken(Usuario Usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, Usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, Usuario.Email),
            new Claim(ClaimTypes.Role, Usuario.Roles.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<Usuario> RegisterUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
