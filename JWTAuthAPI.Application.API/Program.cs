using JWTAuthAPI.Application.API;
using JWTAuthAPI.Application.API.Middlewares;
using JWTAuthAPI.Application.Core;
using JWTAuthAPI.Application.Infrastructure;
using JWTAuthAPI.Application.Infrastructure.Data;
using JWTAuthAPI.Application.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCoreServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    await SeedDatabaseAsync(app);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMyCorsPolicy();
app.UseJwtAuthorization();
app.MapControllers();
app.Run();

static async Task SeedDatabaseAsync(IHost app)
{
    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>();
    await initialiser.InitializeDBAsync();
    await initialiser.SeedDBAsync();
}