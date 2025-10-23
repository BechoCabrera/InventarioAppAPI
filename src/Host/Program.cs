using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using Mapster;
// ✅ Core y Application
using InventarioBackend.src.Core.Application._Common.Mappings;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.Services;
using InventarioBackend.src.Core.Application.CashClosings.Interfaces;
using InventarioBackend.src.Core.Application.CashClosings.Services;
using InventarioBackend.src.Core.Application.Clients.Interfaces;
using InventarioBackend.src.Core.Application.Clients.Services;
using InventarioBackend.src.Core.Application.EntitiConfigs.Interfaces;
using InventarioBackend.src.Core.Application.EntitiConfigs.Services;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using InventarioBackend.src.Core.Application.Menu.Services;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Application.Products.Services;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Application.Security.Services;
using InventarioBackend.src.Core.Application.Settings.Services;

// ✅ Domain
using InventarioBackend.src.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.CashClosings.Interfaces;
using InventarioBackend.src.Core.Domain.Clients.Interfaces;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Interfaces;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Settings.Interfaces;

// ✅ Infrastructure
using InventarioBackend.src.Infrastructure.Data;
using InventarioBackend.src.Infrastructure.Data.Repositories.Billing;

using InventarioBackend.src.Infrastructure.Data.Repositories.EntitiConfigs;
using InventarioBackend.src.Infrastructure.Data.Repositories.Products;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;
using InventarioBackend.src.Infrastructure.Data.Repositories.Settings;
using Infrastructure.Data.Repositories.Products;
using InventarioBackend.Core.Application._Common.Mappings;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.Infrastructure.Data.Repositories.Billing;
using InventarioBackend.Infrastructure.Data.Repositories.Clients;
using InventarioBackend.src.Core.Domain.CashClosings.Services;

var builder = WebApplication.CreateBuilder(args);

// ================= JWT ======================
var jwtSecret = builder.Configuration["Jwt:Secret"];
Console.WriteLine($"Jwt:Secret: {jwtSecret ?? "NULL"}");
if (string.IsNullOrEmpty(jwtSecret))
    throw new Exception("JWT Secret no configurada en appsettings.json");

var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // poner en true para prod
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

// ================= DbContext ======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// ================= DI (Inyección de dependencias) ======================
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();

builder.Services.AddScoped<IConsecutiveSettingsRepository, ConsecutiveSettingsRepository>();
builder.Services.AddScoped<ConsecutiveSettingsService>();

builder.Services.AddScoped<IEntitiConfigService, EntitiConfigService>();
builder.Services.AddScoped<IEntitiConfigRepository, EntitiConfigRepository>();

builder.Services.AddScoped<ICashClosingService, CashClosingService>();
builder.Services.AddScoped<ICashClosingRepository, CashClosingRepository>();

builder.Services.AddScoped<IInvoiceCancellationService, InvoiceCancellationService>();
builder.Services.AddScoped<IInvoiceCancellationRepository, InvoiceCancellationRepository>();

builder.Services.AddHttpContextAccessor();

// ================= Mapster ======================
TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMapping).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(ClientMapping).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(CashClosingMapping).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(InvoiceMapping).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(ConsecutiveSettingsMapping).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(EntitiConfigMapping).Assembly);

// ================= Controllers y JSON ======================
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
});

// ================= Swagger ======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "InventarioBackend",
        Version = "v1"
    });
});

// ================= CORS ======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "http://securityreport.fisegroup.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// ================= Pipeline ======================
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventarioBackend v1");
});

app.UseHttpsRedirection();
app.UseCors("AllowAngularClient");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
