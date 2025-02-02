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

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public string RequiredRole { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var jwtHandler = new JwtSecurityTokenHandler();
        if (jwtHandler.CanReadToken(token))
        {
            var principal = ValidateJwtToken(token, configuration);
            if (principal != null)
            {
                context.HttpContext.User = principal;
                if (IsUserInRole(principal, RequiredRole))
                {
                    return;
                }
            }
        }

        var cognitoPrincipal = ValidateCognitoToken(token, configuration).GetAwaiter().GetResult();
        if (cognitoPrincipal != null)
        {
            context.HttpContext.User = cognitoPrincipal;
            if (IsUserInRole(cognitoPrincipal, RequiredRole))
            {
                return;
            }
        }

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
            return null;
        }
    }

    private async Task<ClaimsPrincipal> ValidateCognitoToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwksUrl = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}/.well-known/jwks.json";
        var httpClient = new HttpClient();
        var jwksResponse = await httpClient.GetStringAsync(jwksUrl);
        var jwks = JsonConvert.DeserializeObject<Jwks>(jwksResponse);

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var kid = jwtToken.Header["kid"].ToString();
        var key = jwks.Keys.FirstOrDefault(k => k.Kid == kid);
        if (key == null)
        {
            return null;
        }

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
            return null;
        }
    }

    private bool IsUserInRole(ClaimsPrincipal principal, string role)
    {
        if (principal == null || string.IsNullOrEmpty(role))
        {
            return false;
        }

        // Verificar si el usuario tiene el rol requerido en los claims
        var roleClaims = principal.FindAll(ClaimTypes.Role).Select(c => c.Value);
        if (roleClaims.Contains(role))
        {
            return true;
        }

        // Verificar si el usuario pertenece al grupo de Cognito
        var cognitoGroups = principal.FindAll("cognito:groups").Select(c => c.Value);
        return cognitoGroups.Contains(role);
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
