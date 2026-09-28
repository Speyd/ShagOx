using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiController 
    : ControllerBase
{
    protected long UserId =>
        long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}