using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.DTOs.Orders.Core.Status;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Payment;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Status;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Orders;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderCommandsController
    : ApiOwnerExistsController
{
    private readonly IOrderCreateService _createService;
    private readonly IOrderPaymentService _paymentService;
    private readonly IOrderStatusService _statusService;


    public OrderCommandsController(
        IOrderCreateService createService,
        IOrderPaymentService paymentService,
        IOrderStatusService statusService,
        IOrderExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(existsAdvertRepository, userQueryService, userContext)
    {
        _createService = createService;
        _paymentService = paymentService;
        _statusService = statusService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] OrderCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId!.Value);

        return result.ToActionResult();
    }

    /// <summary>
    /// Повертає діюче посилання на оплату або створює нове
    /// (у т.ч. для замовлення, скасованого через прострочену оплату).
    /// </summary>
    [HttpPost("{id:long}/payment")]
    public async Task<IActionResult> Pay(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _paymentService
            .GetOrCreatePaymentAsync(id, cancellationToken);

        return result.ToActionResult();
    }

    // Права перевіряє сервіс: покупець — власник замовлення,
    // продавець — власник усіх товарів у ньому.

    /// <summary>Покупець скасовує замовлення (до відправки).</summary>
    [HttpPost("{id:long}/cancel")]
    public async Task<IActionResult> Cancel(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _statusService
            .CancelByBuyerAsync(id, UserId!.Value, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>Продавець підтверджує нове замовлення.</summary>
    [HttpPost("{id:long}/confirm")]
    public async Task<IActionResult> Confirm(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _statusService
            .ConfirmBySellerAsync(id, UserId!.Value, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>Продавець відхиляє замовлення (оплачене — з поверненням коштів).</summary>
    [HttpPost("{id:long}/reject")]
    public async Task<IActionResult> Reject(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _statusService
            .RejectBySellerAsync(id, UserId!.Value, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>Продавець відправляє замовлення (ТТН) або готує його до самовивозу.</summary>
    [HttpPost("{id:long}/ship")]
    public async Task<IActionResult> Ship(
        [FromRoute] long id,
        [FromBody] OrderShipRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _statusService
            .ShipBySellerAsync(id, UserId!.Value, request.TrackingNumber, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>Покупець або продавець підтверджує отримання замовлення.</summary>
    [HttpPost("{id:long}/complete")]
    public async Task<IActionResult> Complete(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _statusService
            .CompleteAsync(id, UserId!.Value, cancellationToken);

        return result.ToActionResult();
    }
}
