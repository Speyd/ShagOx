using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationExistsRepository
    : ExistsTranslationRepository<Status,
        StatusTranslation>,
      IStatusTranslationExistsRepository
{
    public StatusTranslationExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    protected override IQueryable<StatusTranslation> ApplyIncludes(
         IQueryable<StatusTranslation> query)
    {
        return query.WithIncludes();
    }
}