using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationQueryRepository
    : QueryTranslationRepository<StatusTranslation,
        StatusTranslationSearchFilter>,
      IStatusTranslationQueryRepository
{
    public StatusTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<StatusTranslation> ApplyIncludes(
         IQueryable<StatusTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<StatusTranslation> ApplyFilter(
       IQueryable<StatusTranslation> query,
       StatusTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public override async Task<StatusTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        return await _db.StatusTranslations
          .WithIncludes()
          .FirstOrDefaultAsync(x => 
            x.Translatable.Code == identificator &&
            x.Language == language);
    }

    public override async Task<PagedResult<StatusTranslation>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        return await _db.StatusTranslations
           .WithIncludes()
           .Where(x => x.Language == language)
           .ToPagedResultAsync(pagination);
    }
}