using InventarioBackend.src.Infrastructure.Data;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Application.Security.Services;
using Microsoft.EntityFrameworkCore;
using InventarioBackend.src.Core.Infrastructure.Data.Security;
var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión (ajústala a tu base de datos)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=InventarioDB;Trusted_Connection=True;";

// Agregar DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Aquí agregar ProductRepository, etc.
// builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Registrar servicios de aplicación
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
// builder.Services.AddScoped<IProductService, ProductService>();

// Configurar autenticación JWT (ejemplo básico)
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"] ?? "TuClaveSuperSecretaDeAlMenos32Caracteres!");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // En producción poner true
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Opcional: configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.WithOrigins("http://localhost:4200")  // Origen exacto que usas en Angular
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();  // Muy importante para que funcione con credenciales
    });
});
builder.Services.AddScoped<ITokenService, TokenService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
