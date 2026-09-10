using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers;
public abstract class ApiController 
    : ControllerBase
{
    protected int UserId =>
        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}