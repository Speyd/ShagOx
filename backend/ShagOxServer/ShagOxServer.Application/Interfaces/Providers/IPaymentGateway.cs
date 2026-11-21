using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Providers;

/// <summary>
/// Операції з уже створеним рахунком у платіжного провайдера.
/// Винесено окремо від <see cref="IPaymentProvider"/>, бо провайдер залежить
/// від обробника вебхуків, а сервіси обробки платежів — від цього шлюзу
/// (інакше вийшла б циклічна залежність у DI).
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    /// Повне повернення коштів за оплаченим рахунком.
    /// Успіх означає, що банк прийняв запит (повернення може ще оброблятися).
    /// </summary>
    Task<Result<bool>> RefundAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Анулює неоплачений рахунок, щоб за посиланням більше не можна було заплатити.
    /// </summary>
    Task<Result<bool>> CancelInvoiceAsync(
        string invoiceId,
        CancellationToken cancellationToken = default);
}
