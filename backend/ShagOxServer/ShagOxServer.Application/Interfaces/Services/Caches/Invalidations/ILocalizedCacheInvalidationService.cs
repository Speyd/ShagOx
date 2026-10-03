using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ILocalizedCacheInvalidationService<TEntity>
     where TEntity : BaseEntity
{
    Task InvalidateLanguageAsync(
       string language);
}