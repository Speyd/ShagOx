using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Images;

[ApiController]
[Route("api/advertisement-images")]
public class AdvertisementImageQueriesController 
    : BaseQueryController<AdvertisementImageDto,
        AdvertisementImageSearchFilter>
{
    public AdvertisementImageQueriesController(
        IAdvertisementImageQueryService queryService
    )
        : base(queryService)
    {
    }
}
