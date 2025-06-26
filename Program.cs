using Microsoft.EntityFrameworkCore;
using WSCalculadoraAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración directa de la cadena de conexión interna
var connectionString = "Host=dpg-d1erbobe5dus73953qf0-a;Port=5432;" +
                      "Database=calculadora_v6c8;Username=root;" +
                      "Password=xzzzerg7c3CFzFhhvxutzFzXSzUiMjsN;" +
                      "SSL Mode=Prefer;Trust Server Certificate=true";

builder.Services.AddDbContext<CalculadoraContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicación de migraciones con verificación robusta
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CalculadoraContext>();
    try
    {
        Console.WriteLine("Verificando conexión a la base de datos...");
        if (await db.Database.CanConnectAsync())
        {
            Console.WriteLine("Aplicando migraciones...");
            await db.Database.MigrateAsync();
            Console.WriteLine("Migraciones aplicadas correctamente");
        }
        else
        {
            Console.WriteLine("No se pudo conectar a la base de datos");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error en migraciones: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "5000"}");