using Scalar.AspNetCore;
using Serilog;
using sloth.API.Extensions;
using sloth.API.Middleware;
using sloth.Application.Extensions;
using sloth.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register API Layer
builder.AddPresentation();
builder.AddAuthentication(builder.Configuration);
// Register Application Layer
builder.Services.AddApplication();
// Register Infrastructure Layer
builder.Services.AddInfractructure(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Run seeder to inject initial values
await app.RunSeed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.AddCors(builder.Configuration);

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Sloth")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    /* Display scalar url in dev mode */
    // Extract the first https URL from the "urls" configuration
    var urls = builder.Configuration["urls"]?.Split(';') ?? new[] { "https://localhost:5001" };
    var baseAddress = urls.FirstOrDefault(url => url.StartsWith("https")) ?? "https://localhost:5001";

    var fullUrl = $"{baseAddress.TrimEnd('/')}/scalar/v1";
    Log.Information("Scalar API Reference is available at: {Url}", fullUrl);
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
