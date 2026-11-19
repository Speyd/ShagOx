using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Orders;

[ApiController]
[Route("api/orders")]
//[Authorize]
public class OrderCommandsController
    : ApiOwnerExistsController
{
    private readonly IOrderCreateService _createService;


    public OrderCommandsController(
        IOrderCreateService createService,
        IOrderExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(existsAdvertRepository, userQueryService, userContext)
    {
        _createService = createService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] OrderCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId!.Value);

        return result.ToActionResult();
    }
}
