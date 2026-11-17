using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Filters.Deliveries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations;
public class DeliveryProviderTranslationQueryRepository
    : SearchTranslationRepository<DeliveryProvider,
        DeliveryProviderTranslation,
        DeliveryProviderTranslationSearchFilter>,
      IDeliveryProviderTranslationQueryRepository
{
    public DeliveryProviderTranslationQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    protected override IQueryable<DeliveryProviderTranslation> ApplyIncludes(
         IQueryable<DeliveryProviderTranslation> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<DeliveryProviderTranslation> ApplyFilter(
       IQueryable<DeliveryProviderTranslation> query,
       DeliveryProviderTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    protected override IQueryable<DeliveryProviderTranslation> ApplyIdentificatorFilter(
        IQueryable<DeliveryProviderTranslation> query,
        string identificator)
    {
        return query.Where(x =>
            x.Translatable.Code == identificator);
    }
}
