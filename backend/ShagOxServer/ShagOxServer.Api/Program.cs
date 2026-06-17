using Microsoft.AspNetCore.Identity;
using SchagoxServer.Api.DependencyInjection;
using ShagOxServer.Domain.Entities.Account;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

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
