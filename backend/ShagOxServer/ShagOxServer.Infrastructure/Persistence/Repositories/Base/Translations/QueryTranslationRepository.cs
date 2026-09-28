using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class QueryTranslationRepository<TEntity, TTranslator, TFilter>
    : QueryRepository<TTranslator, TFilter>, 
    ITranslationQueryRepository<TEntity, TTranslator, TFilter>
    where TEntity: BaseTranslatable
    where TTranslator : BaseTranslation<TEntity>
    where TFilter : BaseFilter
{
    public QueryTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<TTranslator?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        IQueryable<TTranslator> query = _db.Set<TTranslator>();

        query = ApplyIncludes(query);

        return await query
          .FirstOrDefaultAsync(x =>
            x.Translatable.GetIdentificator() == identificator &&
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
}