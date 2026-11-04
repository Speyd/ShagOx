using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.AdvertisementImages.Admin;

[ApiController]
[Route("api/admin/advertisement-images")]
[Authorize(Roles = "Admin")]
public class AdvertisementImageAdminQueriesController
    : BaseAdminQueryController<AdvertisementImageDto,
        AdvertisementImageSearchFilter>
{
    public AdvertisementImageAdminQueriesController(
        IAdvertisementImageQueryService queryService
    )
        : base(queryService)
    {
    }
}
