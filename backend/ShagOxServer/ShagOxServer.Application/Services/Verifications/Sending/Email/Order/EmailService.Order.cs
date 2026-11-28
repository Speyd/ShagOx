using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
using ShagOxServer.Application.Resources.Orders.OrderPaidEmail;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping.Order.Create;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string OrderPaidEmailFileName = "Order/OrderPaid.cshtml";


    public async Task<Result<bool>> SendOrderPaidAsync(
        Order order)
    {
        try
        {
            var model = OrderPaidModelMapper.ToDto(
                order,
                _urlBuilder.BuildOrderDetailsUrl(order.Id));

            var dynamicModel = EmailViewModelFactory
                .CreateOrderPaid(model);

            var htmlBody = await _renderer.RenderAsync(
                OrderPaidEmailFileName,
                dynamicModel);

            await SendEmailAsync(
                model.CustomerEmail,
                OrderPaidEmailResources.OrderSuccess,
                htmlBody);

            _logger.LogInformation(
                "Order paid email sent. Order: {OrderNumber}",
                model.OrderNumber);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send order paid email.");

            return Result<bool>.Fail(
                VerificationResources.FailedToSendVerificationEmail);
        }
    }
}
