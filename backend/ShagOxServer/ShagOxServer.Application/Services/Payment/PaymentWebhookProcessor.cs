using ShagOxServer.Application.DTOs.Payment.Webhooks;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payment;
public class PaymentWebhookProcessor 
    : IPaymentWebhookProcessor
{
    private readonly IRepository<PaymentWebhook> _webhookRepository;
    private readonly IPaymentWebhookCommandRepository _webhookCommandRepository;
    private readonly IPaymentWebhookQueryRepository _webhookQueryRepository;
    private readonly IPaymentProcessingService _paymentProcessingService;

    private readonly IUnitOfWork _unitOfWork;


    public PaymentWebhookProcessor(
        IRepository<PaymentWebhook> webhookRepository,
        IPaymentWebhookCommandRepository webhookCommandRepository,
        IPaymentWebhookQueryRepository webhookQueryRepository,
        IPaymentProcessingService paymentProcessingService,
        IUnitOfWork unitOfWork)
    {
        _webhookRepository = webhookRepository;
        _webhookCommandRepository = webhookCommandRepository;
        _webhookQueryRepository = webhookQueryRepository;
        _paymentProcessingService = paymentProcessingService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<bool>> ReceiveAsync(
        MonoWebhook webhook,
        string payload,
        CancellationToken cancellationToken = default)
    {
        if (!string.Equals(
            webhook.Status,
            "success",
            StringComparison.OrdinalIgnoreCase))
        {
            return Result<bool>.Success(true);
        }

        var existing = await _webhookQueryRepository
            .GetByInvoiceIdAsync(webhook.InvoiceId, cancellationToken);

        if (existing is not null)
        {
            return Result<bool>.Success(true);
        }

        var entity = new PaymentWebhook
        {
            InvoiceId = webhook.InvoiceId,
            Status = webhook.Status,
            Payload = payload,
            IsProcessed = false,
            ReceivedAt = DateTime.UtcNow
        };

        _webhookRepository.Add(entity);

        await _unitOfWork.SaveChangesAsync();

        await ProcessPendingAsync(
            webhook.InvoiceId,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task ProcessPendingAsync(
        CancellationToken cancellationToken)
    {
        var webhooks = await _webhookQueryRepository
            .GetUnprocessedAsync(cancellationToken);

        foreach (var webhook in webhooks)
        {
            var result = await _paymentProcessingService
                .ProcessSuccessfulPayment(
                    webhook.InvoiceId, cancellationToken);

            if (!result.IsSuccess)
                continue;

            await _webhookCommandRepository.MarkAsProcessedAsync(
                webhook.Id, cancellationToken);
        }
    }

    public async Task ProcessPendingAsync(
        string invoiceId,
        CancellationToken cancellationToken)
    {
        var webhook = await _webhookQueryRepository
            .GetByInvoiceIdAsync(invoiceId, cancellationToken);

        if (webhook is null)
            return;

        var result = await _paymentProcessingService
            .ProcessSuccessfulPayment(
                webhook.InvoiceId, cancellationToken);
    }
}
