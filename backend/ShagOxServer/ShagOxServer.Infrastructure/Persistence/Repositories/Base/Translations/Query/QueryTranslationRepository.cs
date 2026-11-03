using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations.Query;
public class QueryTranslationRepository<TEntity, TTranslator>
    : QueryRepository<TTranslator>, 
    IQueryTranslationRepository<TEntity, TTranslator>
    where TEntity: BaseTranslatable
    where TTranslator : BaseTranslation<TEntity>
{
    public QueryTranslationRepository(
        ReplicaDbContext db)
        : base(db)
    {
    }

    public virtual async Task<TTranslator?> GetByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);

        query = ApplyIdentificatorFilter(query, 
            identificator);

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Language == language, cancellationToken);
    }

    public virtual async Task<PagedResult<TTranslator>> GetPagedAsync(
        PaginationParams pagination,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);

        return await _db.Set<TTranslator>()
            .WithStatusFilter(status)
           .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TTranslator>.Language)) == language)
           .ToPagedResultAsync(
                pagination,
                (x => x.Id),
                cancellationToken);
    }

    protected virtual IQueryable<TTranslator> ApplyIdentificatorFilter(
        IQueryable<TTranslator> query,
        string identificator)
    {
        return query;
    }

    public virtual async Task<List<BaseTranslationCacheInfo>> GetCacheInfosByTranslatableAsync(
        long translatableId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        return await query
            .WithStatusFilter(status)
            .Where(x => x.TranslatableId == translatableId)
            .Select(x => new BaseTranslationCacheInfo(
                x.Id,
                x.Language))
            .ToListAsync(cancellationToken);
    }
}
