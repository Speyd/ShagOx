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
[Authorize]
public class OrderCommandsController
    : ApiOwnerExistsController
{
    private readonly IOrderCreateService _createService;
    //private readonly IOrderUpdateService _updateService;
    //private readonly IOrderDeleteService _deleteService;


    public OrderCommandsController(
        IOrderCreateService createService,
        //IAdvertisementUpdateService updateService,
        //IAdvertisementDeleteService deleteService,
        IOrderExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(existsAdvertRepository, userQueryService, userContext)
    {
        _createService = createService;
        //_updateService = updateService;
        //_deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] OrderCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId!.Value);

        return result.ToActionResult();
    }

    //[HttpPut("{id:long}")]
    //public async Task<IActionResult> Update(
    //    [FromRoute] long id,
    //    [FromForm] AdvertisementUpdateRequest request)
    //{
    //    var forbidden = await CheckOwnershipAsync(id);
    //    if (forbidden is not null)
    //        return forbidden;

    //    var result = await _updateService
    //        .UpdateAsync(id, request);

    //    return result.ToActionResult();
    //}

    //[HttpDelete("{id:long}")]
    //public async Task<IActionResult> Delete(
    //    [FromRoute] long id)
    //{
    //    var forbidden = await CheckOwnershipAsync(id);
    //    if (forbidden is not null)

    //        return forbidden;
    //    var result = await _deleteService
    //        .SoftDeleteAsync(id);

    //    return result.ToActionResult();
    //}
}
