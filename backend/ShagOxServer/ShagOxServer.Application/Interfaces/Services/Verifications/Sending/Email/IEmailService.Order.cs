using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Sending.Email;
public partial interface IEmailService
{
    Task<Result<bool>> SendOrderPaidAsync(
        Order order);

    Task<Result<bool>> SendOrderConfirmedAsync(
        Order order);
}
