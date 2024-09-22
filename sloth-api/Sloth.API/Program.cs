using Serilog;
using Sloth.API.Extensions;
using Sloth.API.Middlewares;
using Sloth.Application.Extensions;
using Sloth.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Has to be before as database connection is needed
builder.ReadConfiguration();

// Needed before adding other layers as it defines the configuration of the application
var configuration = builder.Services.InitializeInfrastucture(builder.Configuration);

// Declare configured authentication 
builder.Services.AddConfiguredAuthentication(configuration);

// Register API Layer
builder.AddPresentation();
// Register Application Layer
builder.Services.AddApplication();
// Register Infrastructure Layer
builder.Services.AddInfractructure();

var app = builder.Build();

// Run seeder to inject initial values
await app.RunSeed();

app.UseMiddleware<ErrorHandlingMiddleware>();

// TO DO: Add table that will define allowed origins
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials());

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
