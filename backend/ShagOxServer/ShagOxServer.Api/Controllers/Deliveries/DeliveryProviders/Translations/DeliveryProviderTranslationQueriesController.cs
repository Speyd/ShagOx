using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Domain.Filters.Deliveries.Translations;

namespace ShagOxServer.Api.Controllers.Deliveries.DeliveryProviders.Translations;

[ApiController]
[Route("api/delivery-providers/translations")]
public class DeliveryProviderTranslationQueriesController
    : BaseTranslationQueryController<DeliveryProviderTranslationDto,
        DeliveryProviderTranslationSearchFilter>
{
    public DeliveryProviderTranslationQueriesController(
        IDeliveryProviderTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
