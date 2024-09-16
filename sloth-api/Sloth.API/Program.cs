using Serilog;
using Sloth.API.Extensions;
using Sloth.Application.Extensions;
using Sloth.Domain.Entities;
using Sloth.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

// Add Other Layers
builder.Services.AddApplication();

builder.Services.AddInfractructure(builder.Configuration);

// Builds configuration based on DB objects, needs to happen after AddInfractructure(), as it relies on DbContext.
await builder.Services.ConfigureAppConfig();
builder.SetTokenConfig();
builder.SetIdentityConfig();

var app = builder.Build();

// Run seeder to inject initial values
await app.RunSeed();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// Use extension to get only needed endpoints for login etc. 
app.MapGroup("api/auth")
    .MapIdentityApiFilterable<User>(new());

app.UseAuthorization();

app.MapControllers();

app.Run();
