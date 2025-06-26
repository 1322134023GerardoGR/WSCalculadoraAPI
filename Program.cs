using Microsoft.EntityFrameworkCore;
using WSCalculadoraAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la base de datos
builder.Services.AddDbContext<CalculadoraContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("calculadora")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones autom�ticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CalculadoraContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "5000"}");