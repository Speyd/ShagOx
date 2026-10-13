using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches;
using StackExchange.Redis;

namespace ShagOxServer.Application.Services.Caches.Core;
public sealed class UserCacheService 
    : CacheService, IUserCacheService
{
    public UserCacheService(
        [FromKeyedServices("UserRedis")]
        IConnectionMultiplexer multiplexer)
        : base(multiplexer)
    {
    }
}
