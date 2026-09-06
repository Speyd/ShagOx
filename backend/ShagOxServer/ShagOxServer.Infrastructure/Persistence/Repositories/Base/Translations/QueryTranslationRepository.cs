using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class QueryTranslationRepository<T>
    : QueryRepository<T>, IQueryTranslationRepository<T>
    where T : BaseEntity
{
    public QueryTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        return await _db.Set<T>()
           .Where(x => EF.Property<string>(
                    x,
                    nameof(BaseTranslation<T>.Language)) == language)
           .ToPagedResultAsync(pagination);
    }
}