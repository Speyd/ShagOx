using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Avatars;

[ApiController]
[Route("api/avatars")]
public class AvatarQueriesController 
    : BaseQueryController<AvatarDto,
        AvatarSearchFilter>
{
    public AvatarQueriesController(
        IAvatarQueryService queryService
    )
        : base(queryService)
    {
    }
}
