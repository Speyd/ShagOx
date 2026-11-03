using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Avatars;

[ApiController]
[Route("api/admin/avatars")]
[Authorize(Roles = "Admin")]
public class AvatarQueriesAdminController
    : BaseAdminQueryController<AvatarDto,
        AvatarSearchFilter>
{
    public AvatarQueriesAdminController(
        IAvatarQueryService queryService
    )
        : base(queryService)
    {
    }
}
