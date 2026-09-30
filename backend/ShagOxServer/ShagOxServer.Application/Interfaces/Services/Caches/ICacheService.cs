namespace ShagOxServer.Application.Interfaces.Services.Caches;
public interface ICacheService
{
    Task<T?> GetAsync<T>(
        string key);

    Task SetAsync<T>(
        string key, 
        T value, 
        TimeSpan expiration);

    Task RemoveAsync(
        string key);

    Task RemoveByPatternAsync(
        string pattern);
}