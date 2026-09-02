using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationQueryRepository
    : QueryRepository<StatusTranslation>,
      IStatusTranslationQueryRepository
{
    public StatusTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<StatusTranslation>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        return await _db.StatusTranslations
           .WithIncludes()
           .Where(x => x.Language == language)
           .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<StatusTranslation>> Search(
        StatusTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.StatusTranslations
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}