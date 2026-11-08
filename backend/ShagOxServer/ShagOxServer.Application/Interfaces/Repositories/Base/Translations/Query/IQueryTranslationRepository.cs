using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
public interface IQueryTranslationRepository<TEntity, TTranslation>
    : IQueryRepository<TTranslation>
    where TEntity : BaseTranslatable
    where TTranslation : BaseTranslation<TEntity>
{
    Task<PagedResult<TTranslation>> GetPagedAsync(
       PaginationParams pagination,
       string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<TTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<TTranslation?> GetByTranslatableIdAsync(
        long translatableId,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<BaseTranslationCacheInfo>> GetCacheInfosByTranslatableAsync(
        long translatableId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
