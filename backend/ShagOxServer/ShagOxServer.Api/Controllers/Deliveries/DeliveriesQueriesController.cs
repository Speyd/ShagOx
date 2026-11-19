using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Deliveries;

[ApiController]
[Route("api/delivery")]
public class DeliveriesQueriesController 
    : ApiController
{
    private readonly IDeliveryQueriesService _deliveryQueriesService;


    public DeliveriesQueriesController(
        IDeliveryQueriesService deliveryQueriesService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _deliveryQueriesService = deliveryQueriesService;
    }



    [HttpGet("cities")]
    public async Task<IActionResult> GetCities(
        [FromQuery] string providerCode,
        [FromQuery] string citySearch,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _deliveryQueriesService.GetCitiesAsync(
            providerCode,
            citySearch,
            pagination,
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("streets")]
    public async Task<IActionResult> GetStreets(
        [FromQuery] string providerCode,
        [FromQuery] DeliveryStreetSearchRequest request,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _deliveryQueriesService.GetStreetsAsync(
            providerCode,
            request,
            pagination,
            cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("points")]
    public async Task<IActionResult> GetPoints(
        [FromQuery] string providerCode,
        [FromQuery] DeliveryPointSearchRequest request,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _deliveryQueriesService.GetPointsAsync(
            providerCode,
            request,
            pagination,
            cancellationToken);

        return result.ToActionResult();
    }
}
