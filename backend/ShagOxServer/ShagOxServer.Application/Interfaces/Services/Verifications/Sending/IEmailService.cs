using ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
public interface IEmailService
{
    Task<Result<bool>> SendVerificationCodeAsync(
        string email,
        string code);

    Task<Result<bool>> SendOrderPaidEmailAsync(
        Order order);
}
