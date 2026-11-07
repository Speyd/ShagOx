using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Common.Context;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiController 
    : ControllerBase
{
    protected readonly IUserContext UserContext;

    protected long? UserId => UserContext.UserId;

    protected ApiController(IUserContext userContext)
    {
        UserContext = userContext;
    }
}
