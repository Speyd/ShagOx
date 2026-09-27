using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class BaseRepositoryDependencyInjection
{
    public static IServiceCollection AddBaseRepositoryInfrastructure(
       this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>),
            typeof(Repository<>));

        services.AddScoped(typeof(IExistsRepository<>),
            typeof(ExistsRepository<>));

        services.AddScoped(typeof(IQueryRepository<,>),
            typeof(QueryRepository<,>));

        services.AddScoped(typeof(ITranslationExistsRepository<>),
            typeof(ExistsTranslationRepository<>));

        services.AddScoped(typeof(ITranslationQueryRepository<,>),
            typeof(QueryTranslationRepository<,>));

        return services;
    }
}