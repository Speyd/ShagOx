using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payment;
public class PaymentWebhookProcessor 
    : IPaymentWebhookProcessor
{
    private readonly IRepository<PaymentWebhook> _webhookRepository;
    private readonly IPaymentWebhookCommandRepository _webhookCommandRepository;
    private readonly IPaymentWebhookQueryRepository _webhookQueryRepository;
    private readonly IPaymentProcessingService _paymentProcessingService;
    private readonly IPaymentCommandRepository _paymentCommandRepository;

    private readonly IUnitOfWork _unitOfWork;


    public PaymentWebhookProcessor(
        IRepository<PaymentWebhook> webhookRepository,
        IPaymentWebhookCommandRepository webhookCommandRepository,
        IPaymentWebhookQueryRepository webhookQueryRepository,
        IPaymentProcessingService paymentProcessingService,
        IPaymentCommandRepository paymentCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _webhookRepository = webhookRepository;
        _webhookCommandRepository = webhookCommandRepository;
        _webhookQueryRepository = webhookQueryRepository;
        _paymentProcessingService = paymentProcessingService;
        _paymentCommandRepository = paymentCommandRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<bool>> ReceiveAsync(
        WebhookDto webhook,
        string payload,
        CancellationToken cancellationToken = default)
    {
        var existing = await _webhookQueryRepository
            .GetByInvoiceIdAsync(
                webhook.InvoiceId,
                webhook.EnumStatus,
                cancellationToken);

        if (existing is not null)
        {
            return Result<bool>.Success(true);
        }

        var entity = new PaymentWebhook
        {
            InvoiceId = webhook.InvoiceId,
            Status = webhook.EnumStatus,
            Payload = payload,
            IsProcessed = false,
            ReceivedAt = DateTime.UtcNow
        };

        _webhookRepository.Add(entity);

        await _unitOfWork.SaveChangesAsync();

        Console.WriteLine(webhook.EnumStatus);
        Console.WriteLine(webhook.InvoiceId);
        Console.WriteLine(entity.InvoiceId);

        return (webhook.EnumStatus switch
        {
            PaymentStatus.Succeeded => await ProcessPendingAsync(
                entity,
                cancellationToken),

            PaymentStatus.Cancelled or
            PaymentStatus.Failed or
            PaymentStatus.Expired =>
                await ProcessCancelAsync(
                    entity,
                    cancellationToken),

            PaymentStatus.Processing =>
                await MarkProcessingAsync(entity, cancellationToken),

            PaymentStatus.Pending =>
                await MarkPendingAsync(entity.Id, cancellationToken),

            _ => Result<bool>.Success(true)
        });
    }

    private async Task<Result<bool>> MarkProcessingAsync(
        PaymentWebhook webhook,
        CancellationToken cancellationToken = default)
    {
        await _webhookCommandRepository.UpdateStatusAsync(
            webhook.Id,
            PaymentStatus.Processing,
            cancellationToken);

        // Банк почав списання: воркер прострочення дасть такому платежу
        // додатковий час (OrderPaymentPolicy.ProcessingGracePeriod).
        await _paymentCommandRepository.TryMarkAsProcessingAsync(
            webhook.InvoiceId,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> MarkPendingAsync(
         long webhookId,
         CancellationToken cancellationToken = default)
    {
        await _webhookCommandRepository.UpdateStatusAsync(
            webhookId,
            PaymentStatus.Pending,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task ProcessPendingAsync(
        int count,
        CancellationToken cancellationToken)
    {
        var webhooks = await _webhookQueryRepository
            .GetUnprocessedAsync(cancellationToken, count);

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

    public async Task<Result<bool>> ProcessPendingAsync(
        string invoiceId,
        CancellationToken cancellationToken)
    {
        var webhook = await _webhookQueryRepository
            .GetByInvoiceIdAsync(invoiceId, null, cancellationToken);

        if (webhook is null)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Webhook);
        }

        return await ProcessPendingAsync(webhook, cancellationToken);
    }

    public async Task<Result<bool>> ProcessPendingAsync(
        PaymentWebhook webhook,
        CancellationToken cancellationToken)
    {
        return await _paymentProcessingService
            .ProcessSuccessfulPayment(
                webhook.InvoiceId, cancellationToken);
    }

    public async Task<Result<bool>> ProcessCancelAsync(
        string invoiceId,
        CancellationToken cancellationToken)
    {
        var webhook = await _webhookQueryRepository
            .GetByInvoiceIdAsync(invoiceId, null, cancellationToken);

        if (webhook is null)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Webhook);
        }

        return await ProcessCancelAsync(webhook, cancellationToken);
    }

    public async Task<Result<bool>> ProcessCancelAsync(
        PaymentWebhook webhook,
        CancellationToken cancellationToken)
    {
        return await _paymentProcessingService
            .ProcessFailedPayment(
                webhook.InvoiceId, cancellationToken);
    }
}
