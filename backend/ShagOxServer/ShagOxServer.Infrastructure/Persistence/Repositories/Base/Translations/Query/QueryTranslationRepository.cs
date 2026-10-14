using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
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
        string language)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);

        query = ApplyIdentificatorFilter(query, identificator);

        return await query
          .FirstOrDefaultAsync(x =>
            x.Language == language);
    }

    public virtual async Task<PagedResult<TTranslator>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);

        return await _db.Set<TTranslator>()
           .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TTranslator>.Language)) == language)
           .ToPagedResultAsync(pagination);
    }

    protected virtual IQueryable<TTranslator> ApplyIdentificatorFilter(
        IQueryable<TTranslator> query,
        string identificator)
    {
        return query;
    }

    public virtual async Task<List<BaseTranslationCacheInfo>> GetCacheInfosByTranslatableAsync(
        long translatableId)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        return await query
            .Where(x => x.TranslatableId == translatableId)
            .Select(x => new BaseTranslationCacheInfo(
                x.Id,
                x.Language))
            .ToListAsync();
    }
}
