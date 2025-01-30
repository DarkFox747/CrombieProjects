using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models.Entidades;
using Humanizer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CrombieProytecto_V0._2.Service
{
    public interface IAuthService
    {
        Task<Usuario> RegisterUser(string nombre, string username, string email, string password);
        Task<string> Login(string email, string password);
        Task<string> RegisterUserWithCognito(RegistroUsuarioDto registroUsuarioDto);
        Task<string> LoginWithCognito(string email, string password);
        //Task<string> ChangePasswordWithCognito(string email, string oldPassword, string newPassword);
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string hash, string salt);
    }

    public class AuthService : IAuthService
    {
        private readonly ProyectContext _context;
        private readonly IConfiguration _configuration;
        private readonly CognitoUserPool _userPool;
        private readonly IAmazonCognitoIdentityProvider _provider;

        public AuthService(ProyectContext context, IConfiguration configuration, CognitoUserPool userPool, IAmazonCognitoIdentityProvider provider)
        {
            _context = context;
            _configuration = configuration;
            _userPool = userPool;
            _provider = provider;
        }

        // Método para registrar usuario localmente (JWT)
        public async Task<Usuario> RegisterUser(string nombre, string username, string email, string password)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                throw new Exception("Email already exists");

            string salt;
            string hashedPassword = HashPassword(password, out salt);

            var usuario = new Usuario
            {
                Nombre = nombre,
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt,
                Roles = UserRole.Regular,
                CreatedAt = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Método para iniciar sesión localmente (JWT)
        public async Task<string> Login(string email, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                throw new Exception("Credenciales Invalidas");

            if (!VerifyPassword(password, usuario.PasswordHash, usuario.Salt))
                throw new Exception("Credenciales Invalidas");

            // Generar JWT Token
            var token = GenerateJwtToken(usuario);
            return token;
        }

        // Método para registrar usuario con Cognito
        public async Task<string> RegisterUserWithCognito(RegistroUsuarioDto registroUsuarioDto)
        {
            // Registrar el usuario en Cognito
            var clientId = _configuration["AWS:ClientId"];
            var clientSecret = _configuration["AWS:ClientSecret"];
            var secretHash = CalculateSecretHash(registroUsuarioDto.Email, clientId, clientSecret);

            var signUpRequest = new SignUpRequest
            {
                ClientId = clientId,
                Username = registroUsuarioDto.Email, // Usar el email como username
                Password = registroUsuarioDto.Password,
                SecretHash = secretHash, // Incluir el SECRET_HASH
                UserAttributes = new List<AttributeType>
        {
            new AttributeType { Name = "email", Value = registroUsuarioDto.Email },
            new AttributeType { Name = "name", Value = registroUsuarioDto.Nombre }
        }
            };

            var signUpResponse = await _provider.SignUpAsync(signUpRequest).ConfigureAwait(false);

            // Guardar el usuario en la base de datos local
            var usuario = new Usuario
            {
                Nombre = registroUsuarioDto.Nombre,
                Username = registroUsuarioDto.Username,
                Email = registroUsuarioDto.Email,
                PasswordHash = "", // No necesitas guardar la contraseña si usas Cognito
                Salt = "", // No necesitas guardar el salt si usas Cognito
                Roles = UserRole.Regular,
                CreatedAt = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return signUpResponse.UserSub;
        }

        // Método para iniciar sesión con Cognito
        public async Task<string> LoginWithCognito(string email, string password)
        {
            var clientId = _configuration["AWS:ClientId"];
            var clientSecret = _configuration["AWS:ClientSecret"];
            var secretHash = CalculateSecretHash(email, clientId, clientSecret);

            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = clientId,
                AuthParameters = new Dictionary<string, string>
        {
            { "USERNAME", email },
            { "PASSWORD", password },
            { "SECRET_HASH", secretHash } // Incluir el SECRET_HASH
        }
            };

            var authResponse = await _provider.InitiateAuthAsync(authRequest).ConfigureAwait(false);

            if (authResponse.AuthenticationResult != null)
            {
                return authResponse.AuthenticationResult.IdToken;
            }
            else if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
            {
                throw new Exception("New password required.");
            }
            else
            {
                throw new Exception("Authentication failed.");
            }
        }

        // Método para cambiar la contraseña con Cognito
       

        // Métodos para hashear y verificar contraseña (JWT)
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

        // Método para generar JWT Token
        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Roles.ToString())
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

        // Método para calcular el Hash Secreto de usuario (Cognito)
        public static string CalculateSecretHash(string username, string clientId, string clientSecret)
        {
            var dataString = username + clientId;
            var data = Encoding.UTF8.GetBytes(dataString);
            var key = Encoding.UTF8.GetBytes(clientSecret);

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

    }
}