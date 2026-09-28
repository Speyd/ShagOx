using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class QueryTranslationRepository<TEntity, TFilter>
    : QueryRepository<TEntity, TFilter>, 
    ITranslationQueryRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public QueryTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual Task<TEntity?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        throw new NotImplementedException(
         "The GetByIdentificatorAsync method must be overridden in the derived repository.");
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        return await _db.Set<TEntity>()
           .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TEntity>.Language)) == language)
           .ToPagedResultAsync(pagination);
    }
}