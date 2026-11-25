using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;
public static class MonoBankStatusMapper
{
    public static PaymentStatus Map(
    string status)
    {
        return status.ToLowerInvariant() switch
        {
            "created" => PaymentStatus.Pending,
            "processing" => PaymentStatus.Processing,
            "success" => PaymentStatus.Succeeded,
            "failure" => PaymentStatus.Failed,
            "expired" => PaymentStatus.Expired,
            _ => throw new NotSupportedException(
                $"Unknown payment status: {status}")
        };
    }

}
