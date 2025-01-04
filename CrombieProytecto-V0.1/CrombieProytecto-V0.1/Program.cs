using CrombieProytecto_V0._1.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ProyectContext>(builder.Configuration.GetConnectionString("DefaultConnection"));




var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProyectContext>();
    if (dbContext.Database.CanConnect())
    {
        Console.WriteLine("Conexión exitosa a la base de datos.");
        dbContext.Database.EnsureCreated();
    }
    else
    {
        Console.WriteLine("Error: No se pudo conectar a la base de datos.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
