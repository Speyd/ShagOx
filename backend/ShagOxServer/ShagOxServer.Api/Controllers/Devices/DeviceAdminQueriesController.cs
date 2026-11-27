using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Devices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Domain.Filters.Devices;

namespace ShagOxServer.Api.Controllers.Devices;

[ApiController]
[Route("api/admin/devices")]
//[Authorize(Roles = "Admin")]
public class DeviceAdminQueriesController
    : BaseQueryController<DeviceDto,
        DeviceSearchFilter>
{
    public DeviceAdminQueriesController(
        IDeviceQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
