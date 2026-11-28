using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
using ShagOxServer.Application.Resources.Orders.OrderConfirmedEmail;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping.Order.Confirmed;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string OrderConfirmedEmailFileName = "Order/OrderConfirmed.cshtml";


    public async Task<Result<bool>> SendOrderConfirmedAsync(
        Order order)
    {
        try
        {    
            var model = OrderConfirmedMapper.ToDto(
                order,
                _urlBuilder.BuildOrderDetailsUrl(order.Id));

            var dynamicModel = EmailViewModelFactory.CreateOrderConfirmed(model);

            var htmlBody = await _renderer.RenderAsync(
                OrderConfirmedEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                model.CustomerEmail,
                OrderConfirmedEmailResources.OrderConfirmed,
                htmlBody);


            _logger.LogInformation(
                "Order confirmed email sent. Order: {OrderNumber}",
                model.OrderNumber);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send order confirmed email.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }

}
