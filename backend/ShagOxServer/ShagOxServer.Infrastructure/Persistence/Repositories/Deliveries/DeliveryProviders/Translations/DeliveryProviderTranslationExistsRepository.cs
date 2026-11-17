using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
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
        AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<DeliveryProviderTranslation> ApplyIncludes(
         IQueryable<DeliveryProviderTranslation> query)
    {
        return query.WithIncludes();
    }
}
