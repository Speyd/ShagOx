using SchagoxServer.Api.DependencyInjection;
using ShagOxServer.Application;
using ShagOxServer.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddEnumConverter();
builder.Services.AddFrontendPolicy();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
