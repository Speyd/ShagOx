using ShagOxServer.Application.DTOs.Orders.Core.Payment;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Orders.Core.Payment;
public interface IOrderPaymentService
{
    /// <summary>
    /// Повертає діюче посилання на оплату замовлення або створює нове.
    /// Якщо попередній платіж прострочено чи відхилено, замовлення
    /// відновлюється, а товари повторно резервуються на складі.
    /// </summary>
    Task<Result<OrderPaymentResponse>> GetOrCreatePaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default);
}
