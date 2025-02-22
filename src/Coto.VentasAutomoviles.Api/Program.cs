using Coto.VentasAutomoviles.Api.Configurations;
using Coto.VentasAutomoviles.Api.Extensions;
using Coto.VentasAutomoviles.Api.Middlewares;
using Coto.VentasAutomoviles.Api.Service;
using Coto.VentasAutomoviles.Application.Extensions;
using Coto.VentasAutomoviles.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var secretKey = "EstaEsUnaClaveMuySeguraYSecreta123456"; //TODO: Solo por prueba tecnica, si no va en vairables de entornos, secrets

// Se deshabilita HTTPS en el entorno de desarrollo
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5003); // Puerto HTTP
    });
}

// Agregar capas de la aplicación
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

// Configurar autenticación JWT
var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "CotoVentasAutomoviles.Api", //Configuro el emisor
        ValidAudience = "CotoVentasAutomoviles.Apps",       //Configuro destinatarios
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Agregar JwtService como un servicio en el contenedor de dependencias
builder.Services.AddSingleton<JwtService>();

// Agregar servicios adicionales
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddSingleton<JwtService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerConfiguration();

app.Run();