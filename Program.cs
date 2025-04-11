using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using orderflow.orchestrator.Middleware;
using orderflow.orchestrator.Data;
using orderflow.orchestrator.Repository;
using orderflow.orchestrator.Services;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Configure Kestrel URL
builder.WebHost.UseUrls("http://0.0.0.0:5051"); // Orchestrator Service runs on port 5051

// Load configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
if (jwtSettings == null)
{
    throw new ArgumentNullException(nameof(jwtSettings), "JWT settings are missing from configuration.");
}
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new ArgumentNullException("Key is missing in JwtSettings."));

// Add services to container
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Add EF Core with PostgreSQL
builder.Services.AddDbContext<OrderFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                //Console.WriteLine("JWT Challenge: Token validation failed.");
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                //Console.WriteLine($"Token authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// Register app-specific services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Add middleware
app.UseMiddleware<LoggingMiddleware>();

// Enable Swagger in development-like environments
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local") || app.Environment.IsEnvironment("PreProduction"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Request pipeline
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
