using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth;
public static class UserDependencyInjection
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();
        services.AddScoped<IUserExistsRepository, UserExistsRepository>();

        return services;
    }
}