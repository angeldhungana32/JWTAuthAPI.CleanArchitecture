using FluentValidation;
using JWTAuthAPI.Application.API.AuthorizationHandlers;
using JWTAuthAPI.Application.API.Extensions;
using JWTAuthAPI.Application.API.Middlewares;
using JWTAuthAPI.Application.API.Validations;
using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Core.Services;
using JWTAuthAPI.Application.Infrastructure.Data;
using JWTAuthAPI.Application.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();

builder.Services.AddCorsPolicy();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("JWTAuthAPIDb"));

builder.Services.AddSwaggerCustom(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<AuthenticateRequestValidator>();

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IRepositoryActivator, RepositoryActivator>();
builder.Services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMyCorsPolicy();
app.UseJwtAuthorization();
app.MapControllers();
app.Run();