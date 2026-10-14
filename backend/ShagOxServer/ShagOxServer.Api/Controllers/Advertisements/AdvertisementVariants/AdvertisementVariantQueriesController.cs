using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Api.Controllers.Advertisements.AdvertisementVariants;

[ApiController]
[Route("api/advertisement-variants")]
public class AdvertisementVariantQueriesController
    : BaseQueryController<AdvertisementVariantDto,
        AdvertisementVariantSearchFilter>
{
    public AdvertisementVariantQueriesController(
        IAdvertisementVariantQueryService queryService
    )
        : base(queryService)
    {
    }
}
