using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface IEmailService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendOrderPaidAsync(
        Order order);

    Task<Result<bool>> SendOrderConfirmedAsync(
        Order order);

    Task<Result<bool>> SendEnableTwoFactorCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendDisableTwoFactorCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendInitiateTwoFactorCodeAsync(
        string email,
        string code);
}
