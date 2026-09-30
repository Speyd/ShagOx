using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Caches;
public interface ICacheService
{
    Task<T?> GetAsync<T>(
        string key);

    Task<Result<T>> GetOrCreateAsync<T>(
        string key,
        Func<Task<Result<T>>> factory,
        TimeSpan expiration);

    Task SetAsync<T>(
        string key, 
        T value, 
        TimeSpan expiration);

    Task RemoveAsync(
        string key);

    Task RemoveByPatternAsync(
        string pattern);
}