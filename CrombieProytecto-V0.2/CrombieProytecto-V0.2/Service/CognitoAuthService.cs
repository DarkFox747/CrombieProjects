using Amazon.Extensions.CognitoAuthentication;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.Extensions.Configuration;
using Amazon.Runtime;
using Amazon;
using CrombieProytecto_V0._2.Service;
using CrombieProytecto_V0._2.Models.dtos;
using System.Text;
using System.Security.Cryptography;

//Define los métodos para Amazon Cognito
public class CognitoAuthService : ICognitoAuthService
{
    private readonly CognitoUserPool _userPool;
    private readonly IConfiguration _configuration;
    private readonly IAmazonCognitoIdentityProvider _provider;

    public CognitoAuthService(IConfiguration configuration, CognitoUserPool userPool, IAmazonCognitoIdentityProvider provider)
    {
        _configuration = configuration;
        _provider = provider;
        _userPool = userPool;
    }
    //Registro de usuario
    public async Task<string> RegisterUserAsync(RegistroUsuarioDto registroUsuarioDto)
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
    //Login de usuario
    public async Task<string> LoginAsync(string email, string password)
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
    //Cambio de password de usuario
    public async Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword)
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
//Calcula la secrethash de un usuario
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
     