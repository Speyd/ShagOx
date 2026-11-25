using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public class CacheInvalidationDispatcher
    : ICacheInvalidationDispatcher
{
    private readonly IServiceProvider _provider;


    public CacheInvalidationDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }


    public async Task InvalidateAsync(
        IEntityCache entity,
        object cacheContext,
        EntityState state,
        CancellationToken cancellationToken = default)
    {
        var cacheInterface = entity.GetType()
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityCache<,>));

        if (cacheInterface is null)
            return;

        var arguments = cacheInterface.GetGenericArguments();

        var handlerType = typeof(CacheInvalidationHandler<,>)
            .MakeGenericType(arguments);

        var handler = _provider.GetRequiredService(handlerType);

        await ((ICacheInvalidationHandler)handler)
            .InvalidateAsync(cacheContext, state, cancellationToken);
    }
}
