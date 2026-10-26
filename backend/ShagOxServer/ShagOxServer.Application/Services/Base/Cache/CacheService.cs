using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Services.Base.Cache;
public abstract partial class CacheService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly IDataCacheService _cache;

    protected readonly CacheSettings _settings;

    protected virtual bool CacheById => true;

    protected virtual bool CacheBySearch => true;

    protected virtual bool CacheByPaged => true;


    public CacheService(
        IDataCacheService cache,
        IOptions<CacheSettings> settings)
    {
        _cache = cache;
        _settings = settings.Value;
    }
}
