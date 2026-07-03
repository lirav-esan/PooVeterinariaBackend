using Microsoft.EntityFrameworkCore;
using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Core.Services;
using SmallChangeDAW.CORE.Infrastructure.Data;
using SmallChangeDAW.CORE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// =======================================================
// CONFIGURACIÓN DE BASE DE DATOS
// =======================================================
builder.Services.AddDbContext<PatitasVetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PatitasVetConnection")));

// =======================================================
// INYECCIÓN DE DEPENDENCIAS - REPOSITORIES
// =======================================================
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<PacienteRepository>();
builder.Services.AddScoped<RegistroRepository>();
builder.Services.AddScoped(typeof(Repository<>));

// =======================================================
// INYECCIÓN DE DEPENDENCIAS - SERVICIOS
// =======================================================
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IRegistroService, RegistroService>();

// =======================================================
// CONFIGURACIÓN DE CORS
// =======================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:9000", "http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// =======================================================
// LOGGING
// =======================================================
builder.Services.AddLogging();

var app = builder.Build();

// =======================================================
// PIPELINE HTTP
// =======================================================
app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");
app.MapControllers();

app.Run();