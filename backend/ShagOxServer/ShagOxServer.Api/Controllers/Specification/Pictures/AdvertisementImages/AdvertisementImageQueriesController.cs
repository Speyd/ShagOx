using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Images;

[ApiController]
[Route("api/advertisement-images")]
public class AdvertisementImageQueriesController 
    : BaseQueryController<ImageDto,
        ImageSearchFilter>
{
    public AdvertisementImageQueriesController(
        IAdvertisementImageQueryService queryService
    )
        : base(queryService)
    {
    }
}
