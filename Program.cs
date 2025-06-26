using Microsoft.EntityFrameworkCore;
using WSCalculadoraAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .AddKeyPerFile("/etc/secrets", optional: true);

var dbUrl = builder.Configuration["DB_URL_INTERNA"];
var connectionString = dbUrl != null && dbUrl.StartsWith("postgresql://")
    ? ConvertRenderUrlToConnectionString(dbUrl, useSsl: false)
    : builder.Configuration.GetConnectionString("calculadora_v6c8");

builder.Services.AddDbContext<CalculadoraContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CalculadoraContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
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

static string ConvertRenderUrlToConnectionString(string url, bool useSsl = true)
{
    var uri = new Uri(url);
    var userInfo = uri.UserInfo.Split(':');
    return $"Host={uri.Host};Port={uri.Port};Database='calculadora_v6c8';Username={userInfo[0]};Password={userInfo[1]};{(useSsl ? "SSL Mode=Require;Trust Server Certificate=true;" : "")}";
}