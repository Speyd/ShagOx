using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class QueryTranslationRepository<TEnity, TFilter>
    : QueryRepository<TEnity, TFilter>, 
    IQueryTranslationRepository<TEnity, TFilter>
    where TEnity : BaseEntity
    where TFilter : BaseFilter
{
    public QueryTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<PagedResult<TEnity>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        return await _db.Set<TEnity>()
           .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TEnity>.Language)) == language)
           .ToPagedResultAsync(pagination);
    }
}