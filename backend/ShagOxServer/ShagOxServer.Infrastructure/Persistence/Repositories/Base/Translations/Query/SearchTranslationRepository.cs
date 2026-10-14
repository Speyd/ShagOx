using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations.Query;
public class SearchTranslationRepository<TEntity, TTranslator, TFilter>
    : QueryTranslationRepository<TEntity, TTranslator>,
    ISearchTranslationRepository<TEntity, TTranslator, TFilter>
    where TEntity : BaseTranslatable
    where TTranslator : BaseTranslation<TEntity>
    where TFilter : BaseFilter
{
    public SearchTranslationRepository(
        ReplicaDbContext db)
        : base(db)
    {
    }

    public virtual async Task<PagedResult<TTranslator>> SearchAsync(
        TFilter filter,
        PaginationParams pagination)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);
        query = ApplyFilter(query, filter);

        return await query
            .ToPagedResultAsync(pagination);
    }

    protected virtual IQueryable<TTranslator> ApplyFilter(
        IQueryable<TTranslator> query,
        TFilter filter)
    {
        return query;
    }
}
