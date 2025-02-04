using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;

namespace Proyect_BLL.Service
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string RequiredRole { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtener IConfiguration desde el contenedor de dependencias
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            // Obtener el token de la cabecera "Authorization"
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                // Si no hay token, devolver un error 401 (No autorizado)
                context.Result = new UnauthorizedResult();
                return;
            }

            // Verificar si es un token JWT local
            var jwtHandler = new JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(token))
            {
                // Validar el token JWT local
                var principal = ValidateJwtToken(token, configuration);
                if (principal != null)
                {
                    // Si el token JWT es válido, establecer el usuario en el contexto
                    context.HttpContext.User = principal;
                    if (IsUserAuthorized(principal))
                    {
                        return;
                    }
                }
            }

            // Si no es un token JWT local, validar como token de Cognito
            var cognitoPrincipal = ValidateCognitoToken(token, configuration).GetAwaiter().GetResult();
            if (cognitoPrincipal != null)
            {
                // Si el token de Cognito es válido, establecer el usuario en el contexto
                context.HttpContext.User = cognitoPrincipal;
                if (IsUserAuthorized(cognitoPrincipal))
                {
                    return;
                }
            }

            // Si no se puede validar el token, devolver un error 401 (No autorizado)
            context.Result = new UnauthorizedResult();
        }

        private ClaimsPrincipal ValidateJwtToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                // Si la validación falla, devolver null
                return null;
            }
        }

        private async Task<ClaimsPrincipal> ValidateCognitoToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Obtener las claves públicas de Cognito (JWKS)
            var jwksUrl = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}/.well-known/jwks.json";
            var httpClient = new HttpClient();
            var jwksResponse = await httpClient.GetStringAsync(jwksUrl);
            var jwks = JsonConvert.DeserializeObject<Jwks>(jwksResponse);

            // Obtener el kid (Key ID) del token
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var kid = jwtToken.Header["kid"].ToString();

            // Buscar la clave pública correspondiente al kid
            var key = jwks.Keys.FirstOrDefault(k => k.Kid == kid);
            if (key == null)
            {
                return null; // No se encontró la clave pública
            }

            // Convertir la clave pública a un formato que pueda usar JwtSecurityTokenHandler
            var rsa = key.ToRsa();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateIssuer = true,
                ValidIssuer = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}",
                ValidateAudience = true,
                ValidAudience = configuration["AWS:ClientId"],
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                // Si la validación falla, devolver null
                return null;
            }
        }

        private bool IsUserAuthorized(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return false;
            }

            // Si no se requiere un rol específico, cualquier usuario autenticado es autorizado
            if (string.IsNullOrEmpty(RequiredRole))
            {
                return true;
            }

            // Verificar si el usuario tiene el rol requerido en los claims
            var roleClaims = principal.FindAll(ClaimTypes.Role).Select(c => c.Value);
            if (roleClaims.Contains(RequiredRole))
            {
                return true;
            }

            // Verificar si el usuario pertenece al grupo de Cognito
            var cognitoGroups = principal.FindAll("cognito:groups").Select(c => c.Value);
            return cognitoGroups.Contains(RequiredRole);
        }
    }

    // Clase para representar el JWKS (JSON Web Key Set)
    public class Jwks
    {
        public List<Jwk> Keys { get; set; }
    }

    // Clase para representar una clave pública (JWK)
    public class Jwk
    {
        public string Kid { get; set; }
        public string Kty { get; set; }
        public string Alg { get; set; }
        public string N { get; set; }
        public string E { get; set; }

        public RSA ToRsa()
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(new RSAParameters
            {
                Modulus = Base64UrlEncoder.DecodeBytes(N),
                Exponent = Base64UrlEncoder.DecodeBytes(E)
            });
            return rsa;
        }
    }

}