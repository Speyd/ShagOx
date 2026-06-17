using Microsoft.AspNetCore.Identity;
using SchagoxServer.Api.DependencyInjection;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Services;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;
using ShagOxServer.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<
    IAuthService,
    AuthService>();


builder.Services.AddScoped<
    IUserRepository,
    UserRepository>();


builder.Services.AddScoped<
    IPasswordHasher<User>,
    PasswordHasher<User>>();


builder.Services.AddScoped<
    IContactValidator,
    ContactValidator>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
