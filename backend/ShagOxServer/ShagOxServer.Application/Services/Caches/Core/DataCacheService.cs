using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches;
using StackExchange.Redis;

namespace ShagOxServer.Application.Services.Caches.Core;
public sealed class DataCacheService 
    : CacheService, IDataCacheService
{
    public DataCacheService(
        [FromKeyedServices("DataRedis")]
        IConnectionMultiplexer multiplexer)
        : base(multiplexer)
    {
    }
}
