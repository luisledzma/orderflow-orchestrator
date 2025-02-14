using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:5051"); // Orchestrator Service runs on port 5001

var app = builder.Build();

// Enable routing
app.UseRouting();

app.MapControllers();

app.Run();
