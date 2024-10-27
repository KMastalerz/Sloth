using Serilog;
using Sloth.API.Extensions;
using Sloth.API.Middlewares;
using Sloth.Application.Extensions;
using Sloth.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register API Layer
builder.AddPresentation();
builder.AddAuthentication(builder.Configuration);
// Register Application Layer
builder.Services.AddApplication();
// Register Infrastructure Layer
builder.Services.AddInfractructure(builder.Configuration);

var app = builder.Build();

// Run seeder to inject initial values
await app.RunSeed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.AddCors();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
