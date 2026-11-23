using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Orders.OrderPaidEmail;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping.Order.Confirmed;
using ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net;
using System.Net.Mail;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
public partial class EmailService
    : IEmailService
{
    private const string OrderConfirmedEmailFileName = "Order/OrderConfirmed.cshtml";


    public async Task<Result<bool>> SendOrderConfirmedEmailAsync(
        Order order)
    {
        try
        {
            var smtpHost = _emailOptions.SmtpHost
                ?? throw new InvalidOperationException(
                    "SMTP host is not configured.");

            var smtpUser = _emailOptions.Username
                ?? throw new InvalidOperationException(
                    "SMTP username is not configured.");

            var smtpPassword = _emailOptions.Password
                ?? throw new InvalidOperationException(
                    "SMTP password is not configured.");

            var fromEmail = _emailOptions.From
                ?? throw new InvalidOperationException(
                    "Sender email is not configured.");

            var client = new SmtpClient(
                smtpHost,
                _emailOptions.SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    smtpUser,
                    smtpPassword)
            };

            var model = OrderConfirmedMapper.ToDto(
                order,
                _urlBuilder.BuildOrderDetailsUrl(order.Id));

            var dynamicModel = EmailViewModelFactory.Create(model);

            var htmlBody = await _renderer.RenderAsync(
                OrderConfirmedEmailFileName,
                dynamicModel);

            var subject = OrderPaidEmailResources.OrderSuccess;

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(model.CustomerEmail);

            await client.SendMailAsync(message);

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
