using InventarioBackend.src.Infrastructure.Data;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Application.Security.Services;
using Microsoft.EntityFrameworkCore;
using InventarioBackend.Core.Application.Menu.Services;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using System.Security.Claims;
using InventarioBackend.src.Core.Application._Common.Mappings;
using Mapster;

var builder = WebApplication.CreateBuilder(args); // ← Permite detectar entorno correctamente

// Leer la clave JWT
var jwtSecret = builder.Configuration["Jwt:Secret"];
Console.WriteLine($"Jwt:Secret: {jwtSecret ?? "NULL"}");
if (string.IsNullOrEmpty(jwtSecret))
    throw new Exception("JWT Secret no configurada en appsettings.json");

var key = Encoding.ASCII.GetBytes(jwtSecret);

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // true en producción
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});

// Inyección de dependencias
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IMenuService, MenuService>();

// Configuración de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMapping).Assembly);

// Configuración de controladores y JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(
            System.Text.Unicode.UnicodeRanges.All);
    });

// Configuración Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "InventarioBackend",
        Version = "v1"
    });
});

// Política CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Activar Swagger siempre (en desarrollo o producción)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventarioBackend v1");
});

// Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAngularClient");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
