using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Avatars.Admin;

[ApiController]
[Route("api/admin/avatars")]
[Authorize(Roles = "Admin")]
public class AvatarAdminQueriesController
    : BaseAdminQueryController<AvatarDto,
        AvatarSearchFilter>
{
    public AvatarAdminQueriesController(
        IAvatarQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
