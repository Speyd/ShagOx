using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Api.Controllers.Advertisements.AdvertisementVariants.Admin;
[ApiController]
[Route("api/admin/advertisement-variants")]
[Authorize(Roles = "Admin")]
public class AdvertisementVariantAdminQueriesController
    : BaseAdminQueryController<AdvertisementVariantDto,
        AdvertisementVariantSearchFilter>
{
    public AdvertisementVariantAdminQueriesController(
        IAdvertisementVariantQueryService queryService
    )
        : base(queryService)
    {
    }
}
