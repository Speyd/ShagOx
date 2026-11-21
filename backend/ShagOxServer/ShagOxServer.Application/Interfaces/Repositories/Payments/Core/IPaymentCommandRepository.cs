using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
public interface IPaymentCommandRepository
{
    Task<bool> TryMarkAsPaidAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdatePaymentStatusAsync(
        string invoiceId,
        PaymentStatus newStatus,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Переводить платіж у Failed, лише якщо він ще відкритий
    /// (Pending або Processing). Повертає false, якщо платіж уже оброблено.
    /// </summary>
    Task<bool> TryMarkAsFailedAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Pending → Processing (банк почав обробляти оплату).
    /// </summary>
    Task<bool> TryMarkAsProcessingAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Успішний платіж замовлення — за ним повертаються кошти (читає з primary).
    /// </summary>
    Task<Payment?> GetSucceededByOrderIdAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Відкритий платіж замовлення з дійсним посиланням (читає з primary).
    /// </summary>
    Task<Payment?> GetActiveByOrderIdAsync(
        long orderId,
        DateTime now,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Усі відкриті (Pending/Processing) платежі замовлення разом із
    /// замовленням та його товарами (читає з primary).
    /// </summary>
    Task<List<Payment>> GetOpenByOrderIdAsync(
        long orderId,
        CancellationToken cancellationToken = default);
}
