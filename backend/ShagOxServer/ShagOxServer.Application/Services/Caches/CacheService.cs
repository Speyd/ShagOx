using ShagOxServer.Application.Interfaces.Services.Caches;
using StackExchange.Redis;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Caches;
public class CacheService
    : ICacheService
{
    protected readonly IDatabase _db;
    protected readonly IConnectionMultiplexer _server;


    public CacheService(
        IConnectionMultiplexer multiplexer)
    {
        _db = multiplexer.GetDatabase();

        _server = multiplexer;
    }


    public async Task<T?> GetAsync<T>(
        string key)
    {
        var value = await _db.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task SetAsync<T>(
        string key,
        T value, 
        TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(value);

        await _db.StringSetAsync(key, json, expiration);
    }

    public async Task RemoveAsync(
        string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    public async Task RemoveByPatternAsync(
        string pattern)
    {
        foreach (var endpoint in _server.GetEndPoints())
        {
            var server = _server.GetServer(endpoint);

            foreach (var key in server.Keys(pattern: pattern))
            {
                await _db.KeyDeleteAsync(key);
            }
        }
    }
}