using Microsoft.EntityFrameworkCore;
using To_do.Contexto;
using To_do.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS - Configuraci�n m�s permisiva para desarrollo
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });

    // Pol�tica alternativa m�s permisiva para desarrollo
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add new service
builder.Services.AddScoped<TareasService, TareasService>();

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AplicacionDbContexto>(
   options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar la pol�tica de desarrollo que es m�s permisiva
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}
else
{
    app.UseCors("AllowReact");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();