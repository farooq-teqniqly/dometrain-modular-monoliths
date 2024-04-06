using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.Users;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Auth:JwtSecret"])
    .AddAuthorization()
    .AddFastEndpoints()
    .SwaggerDocument();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];

builder.Services.AddBookServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserServices(builder.Configuration, logger, mediatRAssemblies);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.Run();

public partial class Program;
