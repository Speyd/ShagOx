using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations.Query;
public class QueryTranslationRepository<TEntity, Translation>
    : QueryRepository<Translation>, 
    IQueryTranslationRepository<TEntity, Translation>
    where TEntity: BaseTranslatable
    where Translation : BaseTranslation<TEntity>
{
    public QueryTranslationRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public virtual async Task<Translation?> GetByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<Translation> query = _db.Set<Translation>();

        query = ApplyIncludes(query);

        query = ApplyIdentificatorFilter(query, 
            identificator);

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Language == language, cancellationToken);
    }

    public virtual async Task<Translation?> GetByIdentificatorAsync(
        long translatableId,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<Translation> query = _db.Set<Translation>();

        query = ApplyIncludes(query);

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.TranslatableId == translatableId &&
                x.Language == language, 
                cancellationToken);
    }

    public virtual async Task<PagedResult<Translation>> GetPagedAsync(
        PaginationParams pagination,
        string language,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<Translation> query = _db.Set<Translation>();

        query = ApplyIncludes(query);

        return await query
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<Translation>.Language)) == language)
            .ToPagedResultAsync(
                pagination,
                (x => x.Id),
                cancellationToken);
    }

    protected virtual IQueryable<Translation> ApplyIdentificatorFilter(
        IQueryable<Translation> query,
        string identificator)
    {
        return query;
    }

    public virtual async Task<List<TranslationCacheContext>> GetCacheInfosByTranslatableAsync(
        long translatableId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<Translation> query = _db.Set<Translation>();

        return await query
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x => x.TranslatableId == translatableId)
            .Select(x => new TranslationCacheContext(
                x.Id,
                x.Language))
            .ToListAsync(cancellationToken);
    }
}
