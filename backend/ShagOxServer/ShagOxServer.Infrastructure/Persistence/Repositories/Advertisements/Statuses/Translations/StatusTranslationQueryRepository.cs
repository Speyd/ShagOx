using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationQueryRepository
    : QueryTranslationRepository<Status,
        StatusTranslation,
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

    protected override IQueryable<StatusTranslation> ApplyIdentificatorFilter(
        IQueryable<StatusTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}