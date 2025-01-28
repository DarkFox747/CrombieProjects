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
        Task<string> ChangePasswordWithCognito(string email, string oldPassword, string newPassword);
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
        //Método para registrar nuevo usuario
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
        //Método para iniciar sesión
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
        //Método para registrar usuario con Amazon Cognito
        public async Task<string> RegisterUserWithCognito(RegistroUsuarioDto registroUsuarioDto)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _configuration["AWS:ClientId"],
                Username = registroUsuarioDto.Username,
                Password = registroUsuarioDto.Password,
                UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email", Value = registroUsuarioDto.Email },
                        new AttributeType { Name = "name", Value = registroUsuarioDto.Nombre }
                    }
            };
            var signUpResponse = await _provider.SignUpAsync(signUpRequest).ConfigureAwait(false);
            return signUpResponse.UserSub;
        }
        //Método para iniciar sesión con Amazon Cognito
        public async Task<string> LoginWithCognito(string email, string password)
        {
            var secretHash = CalculateSecretHash(email);

            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _configuration["AWS:ClientId"],
                AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", email },
                        { "PASSWORD", password },
                        { "SECRET_HASH", secretHash }
                    }
            };

            var authResponse = await _provider.InitiateAuthAsync(authRequest).ConfigureAwait(false);

            if (authResponse.AuthenticationResult != null)
            {
                return authResponse.AuthenticationResult.IdToken;
            }
            else if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
            {
                // Manejar el caso en el que se requiere un cambio de contraseña temporal
                throw new Exception("New password required.");
            }
            else
            {
                throw new Exception("Authentication failed.");
            }
        }
        //Método para cambiar la contraseña con Amazon Cognito
        public async Task<string> ChangePasswordWithCognito(string email, string oldPassword, string newPassword)
        {
            var secretHash = CalculateSecretHash(email);

            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _configuration["AWS:ClientId"],
                AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", email },
                        { "PASSWORD", oldPassword },
                        { "SECRET_HASH", secretHash }
                    }
            };

            var authResponse = await _provider.InitiateAuthAsync(authRequest).ConfigureAwait(false);

            if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
            {
                var challengeResponse = new RespondToAuthChallengeRequest
                {
                    ChallengeName = ChallengeNameType.NEW_PASSWORD_REQUIRED,
                    ClientId = _configuration["AWS:ClientId"],
                    ChallengeResponses = new Dictionary<string, string>
                        {
                            { "USERNAME", email },
                            { "NEW_PASSWORD", newPassword },
                            { "SECRET_HASH", secretHash }
                        },
                    Session = authResponse.Session
                };

                var challengeResponseResult = await _provider.RespondToAuthChallengeAsync(challengeResponse).ConfigureAwait(false);
                return challengeResponseResult.AuthenticationResult.IdToken;
            }
            else
            {
                throw new Exception("Password change failed.");
            }
        }
        //Método para hashear contraseña
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
        //Método para verificar contraseña
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
        //Método para generar JWT Token
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
        //Método para calcular el Hash Secreto de usuario
        private string CalculateSecretHash(string email)
        {
            var clientId = _configuration["AWS:ClientId"];
            var clientSecret = _configuration["AWS:ClientSecret"];
            var dataString = email + clientId;
            var key = Encoding.UTF8.GetBytes(clientSecret);
            var data = Encoding.UTF8.GetBytes(dataString);

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
