using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations;
public class DeliveryProviderTranslationExistsRepository
    : ExistsTranslationRepository<DeliveryProvider,
        DeliveryProviderTranslation>,
      IDeliveryProviderTranslationExistsRepository
{
    public DeliveryProviderTranslationExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    protected override IQueryable<DeliveryProviderTranslation> ApplyIncludes(
         IQueryable<DeliveryProviderTranslation> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }
}
