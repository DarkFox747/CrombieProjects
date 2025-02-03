using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon;
using DotNetEnv;
using CrombieProytecto_V0._2.Models.dtos;

var builder = WebApplication.CreateBuilder(args);

//env config
Env.Load();
builder.Configuration.AddEnvironmentVariables();
// Configurar AWS
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new BasicAWSCredentials(
    builder.Configuration["AWS:AccessKey"],
    builder.Configuration["AWS:SecretKey"]
);
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();

// Configurar AWS Cognito
builder.Services.AddSingleton<IAmazonCognitoIdentityProvider, AmazonCognitoIdentityProviderClient>(sp =>
{
    var config = new AmazonCognitoIdentityProviderConfig
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
    };
    return new AmazonCognitoIdentityProviderClient(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"],
        config
    );
});
builder.Services.AddSingleton<CognitoUserPool>(sp =>
{
    var provider = sp.GetRequiredService<IAmazonCognitoIdentityProvider>();
    return new CognitoUserPool(
        builder.Configuration["AWS:UserPoolId"],
        builder.Configuration["AWS:ClientId"],
        provider
    );
});

// Configuración de AWS Cognito
builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();
builder.Services.AddSingleton<CognitoUserPool>(provider =>
{
    var clientId = builder.Configuration["AWS:ClientId"];
    var userPoolId = builder.Configuration["AWS:UserPoolId"];
    var providerService = provider.GetRequiredService<IAmazonCognitoIdentityProvider>();
    return new CognitoUserPool(userPoolId, clientId, providerService);
});

// Configuración de JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://cognito-idp.us-east-2.amazonaws.com/us-east-2_IrSjTeGzM"; // Issuer
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validar el issuer
        ValidIssuer = "https://cognito-idp.us-east-2.amazonaws.com/us-east-2_IrSjTeGzM", // Issuer esperado
        ValidateAudience = true, // Validar la audiencia
        ValidAudience = "6jdqiqlge81r1b7bddg4035qob", // Client ID de Cognito
        ValidateLifetime = true, // Validar la expiración del token
        ClockSkew = TimeSpan.Zero // No permitir margen de tiempo para la expiración
    };
});


// Configuración de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// Registrar el servicio de autenticación
builder.Services.AddScoped<IAuthService, AuthService>();

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tu API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});


// Intentar obtener la cadena de conexión desde el secret
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Si no hay un secret disponible, usar la cadena de conexión del archivo .env
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
}

// Registrar el contexto con la cadena de conexión seleccionada
builder.Services.AddSqlServer<ProyectContext>(connectionString);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<WishListService>();
builder.Services.AddScoped<S3Service>();
builder.Services.AddScoped<CategoriaService>();
// Registrar CarritoService
builder.Services.AddScoped<CarritoService>();

// Registrar CompraService
builder.Services.AddScoped<CompraService>();
builder.Services.AddScoped<PaginationService<ProductoDto>>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
